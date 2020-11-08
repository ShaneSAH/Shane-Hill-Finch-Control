using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using FinchAPI;

namespace Project_FinchControl
{

    // #==========================================================#
    // 
    //  Title: Finch Control
    //  Description: Sprint five in the Finch series of missions. 
    //               Persistence/File I/O - Validation, methods,  
    //               enum, tuple, lists. In this sprint the user 
    //               selects a password and login in order to 
    //                proceed to the main Finch menus. 
    //  Application Type: Console
    //  Author: Hill, Shane
    //  Dated Created: 9/28/2020
    //  Last Modified: 11/8/2020
    // 
    // #==========================================================#



    /// <summary>
    /// user commands
    /// </summary>
    public enum Command
    {
        NONE,
        MOVEFORWARD,
        MOVEBACKWARD,
        STOPMOTORS,
        WAIT,
        TURNRIGHT,
        TURNLEFT,
        LEDON,
        LEDOFF,
        GETTEMPERATURE,
        MUSICDANCE,
        DONE
    }

    class Program
    {
        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SetTheme();

            DisplayLoginRegister();

            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }

        /// <summary>
        /// setup the console theme
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        #region Login/Register
        /// <summary>
        /// #=======================================#
        /// #           Password Menu Screen        #
        /// #=======================================#
        /// 
        /// User must enter a register and/or enter a valid login before moving on past this screen. 
        /// Added a simple switch/case validation.
        /// </summary>
        static void DisplayLoginRegister()
        {
            DisplayScreenHeader("Finch Login/Register Screen");
            string userInput;
            string dataPath = @"Data/Logins.txt";

            Console.WriteLine("\tWelcome to the Finch Robot Login Screen. Please login or register to continue.\n");
            Console.WriteLine("\ta) Login");
            Console.WriteLine("\tb) Register");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tc) Delete Registry File (Dev use Only. Will be removed in release 1.0 - Clears usernames/passwords)");

            Console.ForegroundColor = ConsoleColor.Green;
            userInput = Console.ReadLine().ToLower();

            switch (userInput)
            {
                case "a":
                    DisplayLogin();
                    break;

                case "b":

                    DisplayRegisterUser();
                    DisplayLogin();
                    break;

                case "c":
                    File.Delete(dataPath);
                    break;

                default:
                    Console.WriteLine("\n\tUnrecognized entry. Please login or register [a, or b] to continue to the main program. ");
                    DisplayContinuePrompt();
                    DisplayLoginRegister();
                    break;
            }
        }
        /// <summary>
        /// #==========================#
        /// #        Login Screen      #      
        /// #==========================#
        /// 
        /// Method that collects input from user and stores that input as usernmae, password in 
        /// 'IsValidLoginInfo' method. 
        /// </summary>
        static void DisplayLogin()
        {
            string userName;
            string password;
            bool validLogin;

            do
            {
                DisplayScreenHeader("Login");

                Console.WriteLine();
                Console.Write("\tEnter your user name:");
                userName = Console.ReadLine();
                Console.Write("\tEnter your password:");
                password = Console.ReadLine();

                validLogin = IsValidLoginInfo(userName, password);

                Console.WriteLine();
                if (validLogin)
                {
                    Console.WriteLine("\tYou are now logged in.");
                }
                else
                {
                    Console.WriteLine("\tIt appears either the user name or password is incorrect.");
                    Console.Write("\tPlease press enter to try login again or type '");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("quit");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("' to go back to the register screen:");
                    //
                    //If user inputs 'quit' this if statement will display the starting login/register screen and break user out of the login loop. 
                    //
                    if (Console.ReadLine().ToLower() == "quit")
                    {
                        DisplayLoginRegister();
                        break;
                    }
                }
                DisplayContinuePrompt();
            } while (!validLogin);
        }
        /// <summary>
        /// =================
        /// check user login
        /// =================
        /// </summary>
        /// <param name="userName">user name entered</param>
        /// <param name="password">password entered</param>
        /// <returns>true if valid user</returns>
        static bool IsValidLoginInfo(string userName, string password)
        {
            List<(string userName, string password)> registeredUserLoginInfo = new List<(string userName, string password)>();
            bool validUser = false;

            registeredUserLoginInfo = ReadLoginInfoData();

            //
            // loop through the list of registered user login tuples and check each one against the login info
            //
            foreach ((string userName, string password) userLoginInfo in registeredUserLoginInfo)
            {
                if ((userLoginInfo.userName == userName) && (userLoginInfo.password == password))
                {
                    validUser = true;
                    break;
                }
            }

            return validUser;
        }
        /// <summary>
        /// #=================================#
        /// #        Register Screen          #
        /// #=================================#
        /// write login info to data file and validates length and upper/lower case requirements for password. 
        /// </summary>
        static void DisplayRegisterUser()
        {
            string userName;
            bool validResponse;
            string password;
            //string password;

            DisplayScreenHeader("Register");

            Console.Write("\tEnter your user name:");
            userName = Console.ReadLine();

            do
            {
                validResponse = true;
                DisplayScreenHeader("Register");

                Console.WriteLine("\tPassword Requirements: 8-15 characters, at least one uppercase and lowercase letter.\n");
                Console.WriteLine("\tPlease enter password now:");
                password = Console.ReadLine();
                if (!((password.Length >= 8) && (password.Length <= 15)))
                {
                    Console.WriteLine("Please enter a password that is between 8-15 characters.");
                    validResponse = false;
                    DisplayContinuePrompt();
                }
                if (password.Contains(" "))
                {
                    Console.WriteLine("Please enter a password that is between 8-15 characters.");
                    validResponse = false;
                    DisplayContinuePrompt();
                }
                if (!password.Any(char.IsUpper))
                {
                    Console.WriteLine("Please enter a password that contains at least one uppercase letter.");
                    validResponse = false;
                    DisplayContinuePrompt();
                }
                if (!password.Any(char.IsLower))
                {
                    Console.WriteLine("Please enter a password that contains at least one lowercase letter.");
                    validResponse = false;
                    DisplayContinuePrompt();
                }

            } while (!validResponse);

            WriteLoginInfoData(userName, password);

            Console.WriteLine();
            Console.WriteLine("\tYou entered the following username and password which will be saved to the registry.");
            Console.Write("\tUser name: ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($"{userName}\n\t");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Password:");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($"{password}\n");
            Console.ForegroundColor = ConsoleColor.Green;

            DisplayContinuePrompt();
        }

        /// <summary>
        /// ===============================
        /// read login info from data file
        /// ===============================
        /// </summary>
        /// <returns>list of tuple of user name and password</returns>
        static List<(string userName, string password)> ReadLoginInfoData()
        {
            string dataPath = @"Data/Logins.txt";

            string[] loginInfoArray;
            (string userName, string password) loginInfoTuple;

            List<(string userName, string password)> registeredUserLoginInfo = new List<(string userName, string password)>();

            loginInfoArray = File.ReadAllLines(dataPath);

            //
            // loop through the array
            // split the user name and password into a tuple
            // add the tuple to the list
            //
            foreach (string loginInfoText in loginInfoArray)
            {
                //
                // use the Split method to separate the user name and password into an array
                //
                loginInfoArray = loginInfoText.Split(',');

                loginInfoTuple.userName = loginInfoArray[0];
                loginInfoTuple.password = loginInfoArray[1];

                registeredUserLoginInfo.Add(loginInfoTuple);

            }

            return registeredUserLoginInfo;
        }

        /// <summary>
        /// Writes login information to data file. Added "\n" in order to create multiple username/password lines. 
        /// Note: no error or validation checking
        /// </summary>
        static void WriteLoginInfoData(string userName, string password)
        {
            string dataPath = @"Data/Logins.txt";
            string loginInfoText;

            loginInfoText = userName + "," + password;

            //
            // use the AppendAllText method to not overwrite the existing logins
            //
            File.AppendAllText(dataPath, loginInfoText + "\n");
        }
        #endregion

        /// <summary>
        /// #===========================================#
        /// #                 Main Menu                 #
        /// #===========================================#
        /// </summary>
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;

            Finch finchRobot = new Finch();


            Console.WriteLine("\tAbout to connect to the Finch robot. Please be sure the USB cable is connected to the robot and computer now.");

            DisplayConnectFinchRobot(finchRobot);

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\ta) ReConnect Finch Robot (If Disconnected)");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine("\te) User Programming");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(finchRobot);
                        break;
                    case "b":
                        DisplayTalentShowMenuScreen(finchRobot);
                        break;

                    case "c":
                        DataRecorderDisplayMenuScreen(finchRobot);

                        break;

                    case "d":
                        AlarmSystemDisplayMenuScreen(finchRobot);

                        break;

                    case "e":
                        UserProgrammingDisplayMenuScreen(finchRobot);

                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        #region Talent Show

        /// <summary>
        /// #===============================================================#
        /// #                     Talent Show Menu                          #
        /// #===============================================================#
        /// </summary>
        static void DisplayTalentShowMenuScreen(Finch myFinch)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Talent Show Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Light and Sound");
                Console.WriteLine("\tb) Dance Moves");
                Console.WriteLine("\tc) Mix it Up");
                //Console.WriteLine("\td) ");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayLightAndSound(myFinch);
                        break;

                    case "b":
                        DisplayDance(myFinch);

                        break;

                    case "c":
                        DisplayMixingItUp(myFinch);

                        break;

                    //case "d":

                    //    break;

                    case "q":
                        quitTalentShowMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitTalentShowMenu);
        }

        /// <summary>
        /// #===============================================================#
        /// #               Talent Show > Light and Sound                   #
        /// #===============================================================#
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayLightAndSound(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Light and Sound");

            Console.WriteLine("\tThe Finch robot will now show off its glowing talent!");
            DisplayContinuePrompt();

            //
            //Finch performs spool down/up with flashing lights and chirps. 
            //

            for (int lightSoundLevel = 255; lightSoundLevel > 0; lightSoundLevel--)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 100);
            }

            finchRobot.noteOn(880);
            finchRobot.setLED(255, 255, 0);
            finchRobot.wait(100);
            finchRobot.noteOn(780);
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait(100);
            finchRobot.noteOn(700);
            finchRobot.setLED(255, 255, 0);
            finchRobot.wait(100);
            finchRobot.noteOn(880);
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait(100);
            finchRobot.noteOn(780);
            finchRobot.setLED(255, 0, 255);
            finchRobot.wait(100);
            finchRobot.noteOn(880);
            finchRobot.setLED(255, 255, 0);
            finchRobot.wait(100);
            finchRobot.noteOn(780);
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait(100);
            finchRobot.noteOn(700);
            finchRobot.setLED(255, 255, 0);
            finchRobot.wait(100);
            finchRobot.noteOn(880);
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait(100);
            finchRobot.noteOn(780);
            finchRobot.setLED(255, 0, 255);
            finchRobot.wait(100);
            finchRobot.noteOn(700);

            for (int lightSoundLevel = 0; lightSoundLevel < 255; lightSoundLevel++)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 100);
            }

            finchRobot.noteOff();
            finchRobot.setLED(0, 0, 0);
            DisplayMenuPrompt("Talent Show Menu");
        }

        /// <summary>
        /// #=================================================#
        /// #              Talent Show > Dance                #
        /// #=================================================#
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayDance(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Dance");

            Console.WriteLine("\tThe Finch robot will now perform the ballroom dance. Robot style.");
            DisplayContinuePrompt();

            //
            //Loop that repeats a basic ballroom line, twirl and ending flourish after loop. 
            //
            for (int i = 0; i < 3; i++)
            {
                finchRobot.setMotors(205, 102);
                finchRobot.wait(500);
                finchRobot.setMotors(102, 205);
                finchRobot.wait(500);
                finchRobot.setMotors(0, 102);
                finchRobot.wait(5000);
                finchRobot.setMotors(-205, -205);
                finchRobot.wait(1000);
                finchRobot.setMotors(205, 205);
                finchRobot.wait(1000);
            }
            finchRobot.setMotors(102, -205);
            finchRobot.wait(2000);
            finchRobot.setMotors(-205, 102);
            finchRobot.wait(2000);
            finchRobot.setMotors(0, 0);

            DisplayMenuPrompt("Talent Show Menu");
        }

        /// <summary>
        /// #========================================================#
        /// #              Talent Show > Mixing it Up                #
        /// #========================================================#
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayMixingItUp(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Mixing It Up");

            Console.WriteLine("\tThe Finch robot will now perform a famous cantina song from a galaxy far, far away...and dance!");
            DisplayContinuePrompt();

            //
            //Music, lights and dance commences. 
            //Each section is broken into individual staves to help keep the code organized. 
            //
            //music staff one 
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait(2000);
            finchRobot.setMotors(50, 50);
            finchRobot.noteOn(880);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 255);
            finchRobot.noteOn(1174);
            finchRobot.wait(300);
            finchRobot.setLED(0, 0, 255);
            finchRobot.noteOn(880);
            finchRobot.setMotors(-50, -50);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 255);
            finchRobot.noteOn(1174);
            finchRobot.wait(300);
            finchRobot.setMotors(50, 50);
            finchRobot.setLED(0, 0, 255);
            finchRobot.noteOn(880);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 255);
            finchRobot.noteOn(1174);
            finchRobot.wait(300);
            finchRobot.setMotors(-50, -50);
            finchRobot.setLED(0, 0, 255);
            finchRobot.noteOn(880);
            finchRobot.wait(200);
            finchRobot.noteOn(830);
            finchRobot.wait(200);
            finchRobot.setMotors(50, -50);
            finchRobot.noteOn(880);
            finchRobot.wait(300);

            //music staff two
            finchRobot.noteOn(880);
            finchRobot.wait(200);
            finchRobot.noteOn(830);
            finchRobot.wait(200);
            finchRobot.noteOn(880);
            finchRobot.wait(200);
            finchRobot.noteOn(830);
            finchRobot.wait(200);
            finchRobot.noteOn(739);
            finchRobot.wait(300);
            finchRobot.noteOn(783);
            finchRobot.wait(200);
            finchRobot.noteOn(739);
            finchRobot.wait(200);
            finchRobot.noteOn(739);
            finchRobot.wait(500);
            finchRobot.noteOn(698);
            finchRobot.wait(300);
            finchRobot.setLED(0, 0, 255);
            finchRobot.setMotors(-50, -50);
            finchRobot.noteOn(880);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 255);
            finchRobot.noteOn(1174);
            finchRobot.wait(300);
            finchRobot.setLED(0, 0, 255);
            finchRobot.noteOn(880);
            finchRobot.setMotors(50, 50);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 255);
            finchRobot.noteOn(1174);
            finchRobot.wait(300);
            finchRobot.setLED(0, 0, 255);
            finchRobot.setMotors(-50, -50);
            finchRobot.noteOn(880);
            finchRobot.wait(300);

            //music staff 3
            finchRobot.setLED(255, 0, 255);
            finchRobot.noteOn(1174);
            finchRobot.wait(300);
            finchRobot.setLED(0, 0, 255);
            finchRobot.setMotors(-50, 50);
            finchRobot.noteOn(880);
            finchRobot.wait(200);
            finchRobot.noteOn(830);
            finchRobot.wait(300);
            finchRobot.noteOn(880);
            finchRobot.wait(300);
            finchRobot.noteOn(783);
            finchRobot.wait(300);
            finchRobot.noteOn(783);
            finchRobot.wait(300);
            finchRobot.noteOn(739);
            finchRobot.wait(600);
            finchRobot.noteOn(783);
            finchRobot.wait(300);
            finchRobot.noteOn(1066);
            finchRobot.wait(300);
            finchRobot.noteOn(987);
            finchRobot.wait(300);
            finchRobot.noteOn(880);
            finchRobot.wait(300);
            finchRobot.noteOn(783);

            //music staff 4
            finchRobot.setLED(0, 0, 255);
            finchRobot.noteOn(880);
            finchRobot.setMotors(50, 50);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 255);
            finchRobot.noteOn(1174);
            finchRobot.wait(300);
            finchRobot.setLED(0, 0, 255);
            finchRobot.noteOn(880);
            finchRobot.setMotors(-50, -50);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 255);
            finchRobot.noteOn(1174);
            finchRobot.wait(300);
            finchRobot.setLED(0, 0, 255);
            finchRobot.noteOn(880);
            finchRobot.setMotors(50, 50);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 255);
            finchRobot.noteOn(1174);
            finchRobot.wait(300);
            finchRobot.setLED(0, 0, 255);
            finchRobot.noteOn(880);
            finchRobot.setMotors(-50, -50);
            finchRobot.wait(200);
            finchRobot.noteOn(830);
            finchRobot.wait(300);
            finchRobot.noteOn(880);
            finchRobot.wait(300);
            finchRobot.setMotors(50, -50);
            finchRobot.noteOn(1066);
            finchRobot.wait(300);
            finchRobot.noteOn(1066);
            finchRobot.wait(300);
            finchRobot.setMotors(50, -50);
            finchRobot.noteOn(880);
            finchRobot.wait(200);
            finchRobot.noteOn(830);
            finchRobot.wait(300);

            //music staff 5
            finchRobot.setLED(0, 125, 255);
            finchRobot.noteOn(698);
            finchRobot.setMotors(-50, -50);
            finchRobot.wait(600);
            finchRobot.setLED(0, 250, 255);
            finchRobot.noteOn(587);
            finchRobot.setMotors(50, 50);
            finchRobot.wait(600);
            finchRobot.setLED(0, 250, 125);
            finchRobot.noteOn(587);
            finchRobot.setMotors(-50, -50);
            finchRobot.wait(600);
            finchRobot.setLED(0, 250, 0);
            finchRobot.noteOn(698);
            finchRobot.setMotors(50, 50);
            finchRobot.wait(600);
            finchRobot.setLED(125, 255, 0);
            finchRobot.noteOn(880);
            finchRobot.setMotors(-50, -50);
            finchRobot.wait(600);
            finchRobot.setLED(255, 125, 0);
            finchRobot.noteOn(1046);
            finchRobot.setMotors(50, 50);
            finchRobot.wait(600);
            finchRobot.setLED(255, 0, 0);
            finchRobot.setMotors(50, -50);
            finchRobot.noteOn(1300);
            finchRobot.wait(300);
            finchRobot.noteOn(1174);
            finchRobot.wait(300);
            finchRobot.noteOn(830);
            finchRobot.wait(300);
            finchRobot.setMotors(-50, 50);
            finchRobot.noteOn(880);
            finchRobot.wait(300);
            finchRobot.setLED(255, 0, 255);
            finchRobot.noteOn(698);
            finchRobot.wait(900);
            finchRobot.noteOff();
            finchRobot.setMotors(0, 0);
            finchRobot.setLED(0, 0, 0);

            DisplayMenuPrompt("Talent Show Menu");
        }

        #endregion

        #region FINCH DATA RECORDER

        /// <summary>
        ///#======================================================#
        ///#               Data Recorder Menu Screen              #
        ///#======================================================#
        ///
        ///Under Development
        /// </summary>
        static void DataRecorderDisplayMenuScreen(Finch finchRobot)
        {
            DisplayScreenHeader("Data Recorder Menu Screen");

            DisplayContinuePrompt();

            string menuChoice;
            bool quitMenu = false;

            int numberOfDataPoints = 0;
            double frequencyOfDataPointsSeconds = 0;
            double[] temperatures = null;
            double[] lightAverage = null;



            do
            {
                DisplayScreenHeader("Data Recorder Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Get number of Data points");
                Console.WriteLine("\tb) Get Frequency of Data points");
                Console.WriteLine("\tc) Get the Temperature Data");
                Console.WriteLine("\td) Get Light Data");
                Console.WriteLine("\te) Display the Data Set");
                Console.WriteLine("\tf) Show Fahrenheit Conversion");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice and validation 
                //
                switch (menuChoice)
                {
                    case "a":
                        numberOfDataPoints = DataRecorderDisplayGetNumberOfDataPoints();
                        break;

                    case "b":
                        frequencyOfDataPointsSeconds = DataRecorderDisplayGetFrequencyOfDataPoints();
                        break;

                    case "c":
                        if (numberOfDataPoints == 0 || frequencyOfDataPointsSeconds == 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("\tPlease enter the number and frequency of data points first.");
                            DisplayContinuePrompt();
                        }
                        else
                        {
                            temperatures = DataRecorderDisplayGetDataSet(numberOfDataPoints, frequencyOfDataPointsSeconds, finchRobot);
                        }
                        break;
                    case "d":
                        if (numberOfDataPoints == 0 || frequencyOfDataPointsSeconds == 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("\tPlease enter the number and frequency of data points first.");
                            DisplayContinuePrompt();
                        }
                        else
                        {
                            lightAverage = DataRecorderDisplayGetLightData(numberOfDataPoints, frequencyOfDataPointsSeconds, finchRobot);
                        }
                        break;
                    case "e":

                        DataRecorderDisplayGetDataSet(temperatures, lightAverage);

                        break;
                    case "f":

                        ConvertCelsiusToFahrenheit(temperatures);

                        break;


                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitMenu);
        }
        /// <summary>
        /// #=========================================================#
        /// #       Data Recorder > Get Number of Data Points         #
        /// #=========================================================#
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>Get Number of user Data Points</returns>
        static int DataRecorderDisplayGetNumberOfDataPoints()
        {
            int numberOfDataPoints;
            string dataPointsResponse;
            do
            {
                DisplayScreenHeader("Number of Data Points");

                Console.Write("\tEnter the number of data points:");
                int.TryParse(Console.ReadLine(), out numberOfDataPoints);

                Console.WriteLine();
                Console.WriteLine($"\tNumber of Data Points: {numberOfDataPoints}");
                Console.WriteLine("\tIs this the correct amount? [yes or no]");
                dataPointsResponse = Console.ReadLine().ToLower();

                //
                //Validation Process - User Confirmation
                //
                switch (dataPointsResponse)
                {
                    case "yes":
                        Console.Clear();
                        Console.WriteLine("\n\tYou selected 'yes'. Returning to the data recorder menu for next step.");
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter the correct number of Data Points.[numeric value]");
                        DisplayContinuePrompt();
                        break;

                }

            } while (dataPointsResponse != "yes");

            DisplayContinuePrompt();

            return numberOfDataPoints;
        }

        /// <summary>
        /// #=======================================================================#
        /// #          Data Recorder > Get the Frequency of Data Points             #
        /// #=======================================================================#
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>Get Frequency of Data Points from User</returns>
        static double DataRecorderDisplayGetFrequencyOfDataPoints()
        {
            double frequencyOfDataPoints;
            string frequencyDataPointsResponse;
            do
            {
                DisplayScreenHeader("Frequency of Data Points");

                Console.Write("\tEnter the frequency of Data Points:");
                double.TryParse(Console.ReadLine(), out frequencyOfDataPoints);

                Console.WriteLine();
                Console.WriteLine($"\tFrequency of Data Points: {frequencyOfDataPoints}");
                Console.WriteLine("\tIs this the correct amount? [yes or no]");
                frequencyDataPointsResponse = Console.ReadLine().ToLower();

                //
                //Validation Process - User Confirmation
                //
                switch (frequencyDataPointsResponse)
                {
                    case "yes":
                        Console.Clear();
                        Console.WriteLine("\n\tYou selected 'yes'. Returning to the data recorder menu for next step.");
                        break;


                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter the correct frequency of Data Points. [numeric value]");
                        DisplayContinuePrompt();
                        break;
                }

            } while (frequencyDataPointsResponse != "yes");

            DisplayContinuePrompt();

            return frequencyOfDataPoints;
        }

        /// <summary>
        /// #========================================================#
        /// #         Data Recorder > Get the Temperature Data       #
        /// #========================================================#
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>Get Temperature Data from user</returns>
        static double[] DataRecorderDisplayGetDataSet(int numberOfDataPoints, double frequencyOfDataPointsSeconds, Finch finchRobot)
        {
            double[] temperatures = new double[numberOfDataPoints];

            DisplayScreenHeader("Get Temperature Data Set");

            Console.WriteLine($"\tNumber of Data Points: {numberOfDataPoints}");
            Console.WriteLine($"\tFrequency of Data Points {frequencyOfDataPointsSeconds}");
            Console.WriteLine();
            Console.WriteLine("The Finch Robot is ready to record the temperature data. Press any key to begin.");
            Console.ReadKey();

            //
            //Begins temperature sensor recording
            //
            double temperature;
            int frequencyOfDataPointsMilliseconds;
            for (int index = 0; index < numberOfDataPoints; index++)
            {
                temperature = finchRobot.getTemperature();
                finchRobot.setLED(255, 0, 255);
                Console.WriteLine($"Temperature Reading{index + 1}: {temperature}");
                temperatures[index] = temperature;
                //frequencyofdatapointsmilliseconds = convert.ToInt32(frequencyofdatapointsseconds *1000);
                frequencyOfDataPointsMilliseconds = (int)(frequencyOfDataPointsSeconds * 1000);
                finchRobot.wait(frequencyOfDataPointsMilliseconds);
            }

            //
            //LED off, sorts array for table display, notifies user data collection complete
            //
            finchRobot.setLED(0, 0, 0);
            Array.Sort(temperatures);
            Console.WriteLine("Temperature Data Collection complete.");
            DisplayContinuePrompt();
            return temperatures;
        }

        /// <summary>
        /// #=======================================================#
        /// #         Data Recorder > Get the Light Data            #
        /// #=======================================================#
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>Get light data from user</returns>
        static double[] DataRecorderDisplayGetLightData(int numberOfDataPoints, double frequencyOfDataPointsSeconds, Finch finchRobot)
        {

            DisplayScreenHeader("\tGet Data Set");

            double[] lightAverage = new double[numberOfDataPoints];
            double leftLightLevel;
            double rightLightLevel;
            int frequencyOfDataPointsMilliseconds;

            Console.WriteLine($"\tNumber of Data Points: {numberOfDataPoints}");
            Console.WriteLine($"\tFrequency of Data Points {frequencyOfDataPointsSeconds}");
            Console.WriteLine();
            Console.WriteLine("The Finch Robot will now collect the first set of light data. Please place Finch in lighted area.");

            //
            //Begins Light Data Recording
            //
            for (int index = 0; index < numberOfDataPoints; index++)
            {
                leftLightLevel = finchRobot.getLeftLightSensor();
                rightLightLevel = finchRobot.getRightLightSensor();
                finchRobot.setLED(255, 0, 255);
                lightAverage[index] = (leftLightLevel + rightLightLevel) / 2;
                Console.WriteLine($"Light Level Reading: {index + 1}: {lightAverage[index]}");
                frequencyOfDataPointsMilliseconds = (int)(frequencyOfDataPointsSeconds * 1000);
                finchRobot.wait(frequencyOfDataPointsMilliseconds);
            }
            //
            //LED off, sorts results for array table display, and notifies user collection complete. 
            //
            finchRobot.setLED(0, 0, 0);
            Array.Sort(lightAverage);
            Console.WriteLine("Light Data Collection complete.");
            DisplayContinuePrompt();

            return lightAverage;
        }

        /// <summary>
        /// #=======================================================#
        /// #        Data Recorder > Display the Data Points        #
        /// #=======================================================#
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>Display Data points</returns>
        static void DataRecorderDisplayGetDataSet(double[] temperatures, double[] lightAverage)
        {

            //
            //Calls DisplayDataTable method
            //
            if (temperatures != null && lightAverage != null)
            {
                DisplayScreenHeader("Data Set");

                DataRecorderDisplayDataTable(temperatures, lightAverage);

                DisplayContinuePrompt();
            }
            else
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("\tPlease get temperature and light data before proceeding.");
                DisplayContinuePrompt();
            }
        }

        //}
        /// <summary>
        /// #===========================================================#
        /// #        Data Recorder > Display Fahrenheit Conversion      #
        /// #===========================================================#
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>Display Farenheit conversion</returns>
        static void ConvertCelsiusToFahrenheit(double[] temperatures)
        {
            //
            //Displays celsius to fahrenheit conversion 
            //
            DisplayScreenHeader("\tFahrenheit Temperature Display");

            double fahrenheit;

            Console.WriteLine(
                         "Reading #".PadLeft(15) +
                         "Celsius".PadLeft(15) +
                         "Fahrenheit".PadLeft(15)
                         );

            for (int index = 0; index < temperatures.Length; index++)
            {
                fahrenheit = ((9 * temperatures[index]) + (32 * 5)) / 5;
                Console.WriteLine(
                (index + 1).ToString().PadLeft(15) +
                temperatures[index].ToString("n2").PadLeft(15) +
                fahrenheit.ToString("n2").PadLeft(15)
                );
            }
            DisplayContinuePrompt();
        }

        //}
        /// <summary>
        /// #==================================================#
        /// #        Data Recorder > Display Data Table        #
        /// #==================================================#
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>Display Data table</returns>
        static void DataRecorderDisplayDataTable(double[] temperatures, double[] lightAverage)
        {
            DisplayScreenHeader("Data Table - Light and Temperature in Celsius");

            Console.WriteLine(
                    "Reading #".PadLeft(15) +
                    "Temperature(C)".PadLeft(15) +
                    "Light Level".PadLeft(15)
                    );

            for (int index = 0; index < temperatures.Length && index < lightAverage.Length; index++)
            {
                Console.WriteLine(
                (index + 1).ToString().PadLeft(15) +
                temperatures[index].ToString("n2").PadLeft(15) +
                lightAverage[index].ToString("n2").PadLeft(15)
                );
            }
            DisplayContinuePrompt();
        }

        #endregion

        #region FINCH ALARM SYSTEM

        /// <summary>
        ///#======================================================#
        ///#              Alarm System Menu Screen                #
        ///#======================================================#
        ///
        ///User selects which submenu they'd like to access for the alarm system. The user must go through all pertinet submenus before allowed access
        ///into the set alarm menu via validation on this menu itself. 
        /// </summary>
        static void AlarmSystemDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            DisplayScreenHeader("Alarm System Menu Screen");


            string menuChoice;
            bool quitMenu = false;

            string sensorsToMonitor = "";
            string rangeType = "";
            int MinMaxThresholdValue = 0;
            int timeToMonitor = 0;
            double temperatureReading;



            do
            {
                DisplayScreenHeader("Alarm System Main Menu Screen");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("**Light sensor alarm function only in this menu. Please see Temperature menu for Temp and Temp/Light Combo alarm.**\n");
                Console.ForegroundColor = ConsoleColor.Green;

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Set Sensors to Monitor");
                Console.WriteLine("\tb) Set Range Type ");
                Console.WriteLine("\tc) Set Min/Max Threshold Value");
                Console.WriteLine("\td) Set Time to Monitor");
                Console.WriteLine("\te) Set Alarm");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("\tf) **New Feature** Temperature Menu");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        sensorsToMonitor = AlarmSystemSetSensorsToMonitor();
                        break;

                    case "b":
                        rangeType = AlarmSystemSetRangeType();
                        break;

                    case "c":
                        MinMaxThresholdValue = AlarmSystemSetThresholdValue(finchRobot, rangeType);

                        break;

                    case "d":
                        timeToMonitor = AlarmSystemSetTimeToMonitor();

                        break;

                    case "e":
                        if (sensorsToMonitor == "" || rangeType == "" || MinMaxThresholdValue == 0 || timeToMonitor == 0)
                        {
                            Console.WriteLine(" Please enter all required values from previous submenus.");
                            DisplayContinuePrompt();
                        }

                        else
                        {
                            AlarmSystemSetAlarm(finchRobot, sensorsToMonitor, rangeType, MinMaxThresholdValue, timeToMonitor);
                        }

                        break;

                    case "f":

                        TemperatureSensorMenuScreen(finchRobot);

                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitMenu);
        }

        /// <summary>
        ///#=========================#
        ///#        Set Alarm        #
        ///#=========================#
        ///
        ///Displays all previous variables and initiates alarm upon user input. If threshold exceeded, LED/Chirps activate and screen message appears.
        /// </summary>
        static void AlarmSystemSetAlarm(Finch finchRobot, string sensorsToMonitor, string rangeType, int minMaxThresholdValue, int timeToMonitor)
        {
            DisplayScreenHeader("Set Alarm");

            Console.WriteLine($"\tLight Sensors to Monitor: {sensorsToMonitor}");
            Console.WriteLine($"\tRange Type: {rangeType}");
            Console.WriteLine($"\tMin/max Threshold Value: {minMaxThresholdValue}");
            Console.WriteLine($"\tTime to Monitor: {timeToMonitor}");

            Console.WriteLine("\tPress any key to set the alarm");
            Console.CursorVisible = false;
            Console.ReadKey();

            bool thresholdExceeded = false;
            int seconds = 1;

            do
            {
                Console.SetCursorPosition(10, 10);
                Console.WriteLine($"\tTime: {seconds++}");
                thresholdExceeded = AlarmSystemThresholdExceeded(finchRobot, sensorsToMonitor, rangeType, minMaxThresholdValue);
                finchRobot.wait(1000);

            } while (!thresholdExceeded && seconds <= timeToMonitor);

            if (thresholdExceeded)
            {
                Console.WriteLine("\tThreshold Exceeded");
                finchRobot.noteOn(600);
                finchRobot.setLED(255, 0, 0);
                finchRobot.wait(500);
                finchRobot.noteOn(900);
                finchRobot.setLED(100, 0, 100);
                finchRobot.wait(500);
                finchRobot.noteOn(600);
                finchRobot.setLED(255, 0, 0);
                finchRobot.wait(500);
                finchRobot.noteOn(900);
                finchRobot.wait(300);
                finchRobot.setLED(0, 0, 0);
                finchRobot.noteOff();
            }
            else
            {
                Console.WriteLine("\tThreshold Not Exceeded");
            }

            DisplayMenuPrompt("Alarm System Menu");
        }


        /// <summary>
        ///#================================#
        ///#       Threshold Exceeded       #
        ///#================================#
        ///Uses Threshold value, rangeType, sensorsToMonitor and bakes these together to determine if the threshold has been exceeded. 
        /// </summary>
        static bool AlarmSystemThresholdExceeded(Finch finchRobot, string sensorsToMonitor, string rangeType, int minMaxThresholdValue)
        {

            int currentLeftLightSensorValue;
            int currentRightLightSensorValue;

            bool thresholdExceeded = false;

            currentLeftLightSensorValue = finchRobot.getLeftLightSensor();
            currentRightLightSensorValue = finchRobot.getRightLightSensor();

            switch (sensorsToMonitor)
            {
                case "left":
                    if (rangeType == "minimum")
                    {
                        thresholdExceeded = currentLeftLightSensorValue < minMaxThresholdValue;

                    }
                    else
                    {
                        thresholdExceeded = currentLeftLightSensorValue > minMaxThresholdValue;

                    }

                    break;


                case "right":
                    if (rangeType == "minimum")
                    {
                        thresholdExceeded = currentRightLightSensorValue < minMaxThresholdValue;

                    }
                    else
                    {
                        thresholdExceeded = currentRightLightSensorValue > minMaxThresholdValue;

                    }

                    break;

                case "both":
                    if (rangeType == "minimum")
                    {
                        thresholdExceeded = (currentLeftLightSensorValue < minMaxThresholdValue) || (currentRightLightSensorValue < minMaxThresholdValue);

                    }
                    else
                    {
                        thresholdExceeded = (currentLeftLightSensorValue > minMaxThresholdValue) || (currentRightLightSensorValue > minMaxThresholdValue);

                    }
                    break;

                default:
                    Console.WriteLine("Unknown Sensor type");
                    Console.ReadKey();
                    break;
            }
            return thresholdExceeded;
        }

        /// <summary>
        ///#=========================================#
        ///#            Get Time To Monitor          #
        ///#=========================================#
        ///
        ///Collects time to monitor from user with validation assuring integer input/output with a tryparse and if else statement. 
        /// </summary>
        static int AlarmSystemSetTimeToMonitor()
        {
            int timeToMonitor;
            string timeToMonitorTestString;
            bool userResponse;

            DisplayScreenHeader("Time To Monitor");

            do
            {
                Console.Write("\tInput time to Monitor:");
                timeToMonitorTestString = Console.ReadLine();
                if (Int32.TryParse(timeToMonitorTestString, out timeToMonitor))
                {
                    userResponse = true;
                    Console.Clear();
                    Console.WriteLine($"\n\n\tTime to monitor is:   {timeToMonitor}");

                }
                else
                {
                    userResponse = false;
                    Console.Clear();
                    Console.WriteLine("\tPlease enter a number.");
                }
            } while (!userResponse);

            DisplayContinuePrompt();

            return timeToMonitor;
        }

        /// <summary>
        ///#=====================================#
        ///#         Set Threshold Value         #
        ///#=====================================#
        ///
        ///User inputs threshold value based on the previously recorded rangeType value with validation of "rangeType" and 
        ///nested validation of minMaxThresholdValue with a tryparse and loop if incorrect to input valid response. 
        /// </summary>
        static int AlarmSystemSetThresholdValue(Finch finchRobot, string rangeType)
        {
            int minMaxThresholdValue = 0;
            string minMaxThresholdValueTestString;
            bool userResponse;
            double finchCelsius;
            double finchFahrenheit;

            DisplayScreenHeader("Min/Max Light Threshold Value");

            Console.WriteLine($"\tThe ambient Left Light Sensor Value: {finchRobot.getLeftLightSensor()}");
            Console.WriteLine($"\tThe ambient Left Light Sensor Value: {finchRobot.getRightLightSensor()}");

            switch (rangeType)
            {
                case "maximum":
                    do
                    {
                        Console.Write($"\n\n\tEnter the {rangeType} threshold value:");
                        minMaxThresholdValueTestString = Console.ReadLine();
                        if (Int32.TryParse(minMaxThresholdValueTestString, out minMaxThresholdValue))
                        {
                            userResponse = true;
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine($"\n\tThe Light Threshold Value you input is: {minMaxThresholdValue}");
                        }
                        else
                        {
                            userResponse = false;
                            Console.WriteLine();
                            Console.WriteLine("\n\tThis is not a number. Please enter a valid threshold value.");
                        }
                    } while (!userResponse);

                    break;

                case "minimum":
                    do
                    {
                        Console.Write($"\n\n\tEnter the {rangeType} threshold value:");
                        minMaxThresholdValueTestString = Console.ReadLine();
                        if (Int32.TryParse(minMaxThresholdValueTestString, out minMaxThresholdValue))
                        {
                            userResponse = true;
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine($"\n\tThe Light Threshold Value you input is: {minMaxThresholdValue}");
                        }

                        else
                        {
                            userResponse = false;
                            Console.WriteLine();
                            Console.WriteLine("\n\tThis is not a number. Please enter a valid threshold value.");
                        }
                    } while (!userResponse);

                    break;

                default:
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("\tPlease enter a rangeType by selecting the range type input submenu before proceeding.\n\n");
                    break;
            }

            DisplayContinuePrompt();

            return minMaxThresholdValue;
        }

        /// <summary>
        ///#===============================================#
        ///#              Range Type From User             #
        ///#===============================================#
        ///
        ///User inputs the range type of maximum or minimum with validation.
        /// </summary>
        static string AlarmSystemSetRangeType()
        {

            DisplayScreenHeader("Range Type Value");

            string rangeType = "";

            do
            {
                Console.Write("\tEnter Range Type [minimum, maximum]:");
                rangeType = Console.ReadLine().ToLower();


                switch (rangeType)
                {
                    case "minimum":
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine($"\tYou entered the following sensors to monitor: {rangeType}");
                        break;

                    case "maximum":
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine($"\tYou entered the following sensors to monitor: {rangeType}");
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter one of the valid responses.\n\n");
                        break;
                }

            } while (rangeType != "minimum" && rangeType != "maximum");

            DisplayContinuePrompt();

            return rangeType;
        }


        /// <summary>
        ///#==============================================#
        ///#              Sensors To Monitor              #
        ///#==============================================#
        ///
        ///User enters which sensor(s) they are using with an echo back to the user before exiting screen. Added validation.
        ///Returns sensorsToMonitor. The Finch has three sensor options that can be selected, Right, Left, or Both. 
        /// </summary>
        static string AlarmSystemSetSensorsToMonitor()
        {
            DisplayScreenHeader("Set which light sensor(s) to monitor");

            string sensorsToMonitor = "";

            do
            {
                Console.Write("\tEnter Light Sensors to Monitor [left, right, both]:");
                sensorsToMonitor = Console.ReadLine().ToLower();

                switch (sensorsToMonitor)
                {
                    case "left":
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine($"\tYou entered the following sensors to monitor: {sensorsToMonitor}");
                        break;

                    case "right":
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine($"\tYou entered the following sensors to monitor: {sensorsToMonitor}");
                        break;

                    case "both":
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine($"\tYou entered the following sensors to monitor: {sensorsToMonitor}");
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter one of the three valid options displayed.\n\n");
                        break;
                }

            } while (sensorsToMonitor != "left" && sensorsToMonitor != "right" && sensorsToMonitor != "both");

            DisplayContinuePrompt();

            return sensorsToMonitor;

        }

        /// <summary>
        ///#=========================================#
        ///#         Temperature Menu Screen         #
        ///#=========================================#
        ///
        ///User enters which submenu they'd like to go to. This menu features the temperature options and light/temp combo alarm. 
        ///Validation is cloned from the main alarm menu and is required for submenu "g" and "h" with unique requirements for each. 
        ///Returns sensorsToMonitor.
        /// </summary>
        static void TemperatureSensorMenuScreen(Finch finchRobot)
        {

            DisplayScreenHeader("Temperature Menu Screen");
            finchRobot.setLED(0, 0, 0);

            string menuChoice;
            bool quitMenu = false;

            string sensorsToMonitor = "";
            string rangeType = "";
            int MinMaxThresholdValue = 0;
            int MinMaxTempThresholdValue = 0;
            int timeToMonitor = 0;
            double temperatureReading = -100;

            do
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                DisplayScreenHeader("Temperature Menu Screen");
                Console.ForegroundColor = ConsoleColor.Green;
                //
                // get user menu choice
                //
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n\t**User Notice:** All submenu options required for combo light/temp alarm.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\ta) Get Temperature Reading");
                Console.WriteLine("\tb) Get Light Sensor Data for Combo Temp/Light Alarm");
                Console.WriteLine("\tc) Set Range Type ");
                Console.WriteLine("\td) Set Min/Max Temp Threshold Value");
                Console.WriteLine("\te) Set Min/Max Light Threshold Value");
                Console.WriteLine("\tf) Set Time to Monitor");
                Console.WriteLine("\tg) Set Combo Light and Temp Alarm");
                Console.WriteLine("\th) Set Temp Only Alarm");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice with validation on case 'g' and 'h'. 
                //
                switch (menuChoice)
                {
                    case "a":
                        temperatureReading = TemperatureSensorReading(finchRobot);
                        break;

                    case "b":
                        sensorsToMonitor = AlarmSystemSetSensorsToMonitorTemp();
                        break;

                    case "c":
                        rangeType = AlarmSystemSetRangeType();
                        break;

                    case "d":
                        MinMaxTempThresholdValue = AlarmSystemSetTempThresholdValue(finchRobot, rangeType);
                        break;

                    case "e":
                        MinMaxThresholdValue = AlarmSystemSetThresholdValue(finchRobot, rangeType);

                        break;

                    case "f":
                        timeToMonitor = AlarmSystemSetTimeToMonitor();

                        break;

                    case "g":
                        if (temperatureReading == -100 || rangeType == "" || MinMaxThresholdValue == 0 || MinMaxTempThresholdValue == 0 || timeToMonitor == 0 ||
                            sensorsToMonitor == "")
                        {
                            Console.WriteLine(" Please enter all required light AND temperature values in the previous submenus.");
                            DisplayContinuePrompt();
                        }

                        else
                        {
                            AlarmSystemSetTemperatureLightAlarm(finchRobot, sensorsToMonitor, temperatureReading, rangeType, MinMaxThresholdValue, timeToMonitor,
                                MinMaxTempThresholdValue);
                        }

                        break;

                    case "h":
                        if (temperatureReading == -100 || rangeType == "" || MinMaxTempThresholdValue == 0 || timeToMonitor == 0)
                        {
                            Console.WriteLine(" Please enter all required temperature values in the previous submenus.");
                            DisplayContinuePrompt();
                        }

                        else
                        {
                            AlarmSystemSetTemperatureOnlyAlarm(finchRobot, temperatureReading, rangeType, timeToMonitor, MinMaxTempThresholdValue);
                        }

                        break;

                    case "q":
                        quitMenu = true;

                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitMenu);

        }
        /// <summary>
        ///#======================================#
        ///#     Set Temperature Only Alarm       #
        ///#======================================#
        ///
        ///User can view previous variables and press any key in order to set alarm with previously selected parameters.
        ///This particular method is the temperature ONLY alarm. LED/Chirps will activate upon threshold exceeded. 
        /// </summary>
        static void AlarmSystemSetTemperatureOnlyAlarm(Finch finchRobot, double temperatureReading, string rangeType, int timeToMonitor,
            int minMaxTempThresholdValue)
        {
            DisplayScreenHeader("Set Alarm");

            Console.WriteLine($"\tTemperature Sensor Reading: {temperatureReading}");
            Console.WriteLine($"\tRange Type: {rangeType}");
            Console.WriteLine($"\tMin/max Temp Threshold Value: {minMaxTempThresholdValue}");
            Console.WriteLine($"\tTime to Monitor: {timeToMonitor}");

            Console.WriteLine("\tPress any key to set the alarm");
            Console.CursorVisible = false;
            Console.ReadKey();

            bool thresholdExceededTemperature = false;
            int seconds = 1;

            do
            {
                Console.SetCursorPosition(10, 10);
                Console.WriteLine($"\tTime: {seconds++}");
                thresholdExceededTemperature = AlarmSystemTemperatureExceeded(finchRobot, rangeType, minMaxTempThresholdValue);
                finchRobot.wait(1000);

            } while (!thresholdExceededTemperature && seconds <= timeToMonitor);

            if (thresholdExceededTemperature)
            {
                Console.WriteLine("\tThreshold Exceeded");
                finchRobot.noteOn(600);
                finchRobot.setLED(255, 0, 0);
                finchRobot.wait(500);
                finchRobot.noteOn(900);
                finchRobot.setLED(100, 0, 100);
                finchRobot.wait(500);
                finchRobot.noteOn(600);
                finchRobot.setLED(255, 0, 0);
                finchRobot.wait(500);
                finchRobot.noteOn(900);
                finchRobot.wait(300);
                finchRobot.setLED(0, 0, 0);
                finchRobot.noteOff();
            }
            else
            {
                Console.WriteLine("\tThreshold Not Exceeded");
            }

            DisplayMenuPrompt("Alarm System Menu");

        }
        /// <summary>
        ///#=================================================#
        ///#     Set Temperature and Light Combo Alarm       #
        ///#=================================================#
        ///
        ///User can view previous variables and press any key in order to set alarm with previously selected parameters. 
        ///This particular method is the Light AND Temperature combo alarm. LEDS and chirps will sound if threshold exceeded. 
        /// </summary>
        static void AlarmSystemSetTemperatureLightAlarm(Finch finchRobot, string sensorsToMonitor, double temperatureReading, string rangeType, int minMaxThresholdValue,
            int timeToMonitor, int minMaxTempThresholdValue)
        {
            DisplayScreenHeader("Set Alarm");

            Console.WriteLine($"\tLight Sensors to Monitor: {sensorsToMonitor}");
            Console.WriteLine($"\tTemperature Sensor Reading: {temperatureReading}");
            Console.WriteLine($"\tRange Type: {rangeType}");
            Console.WriteLine($"\tMin/max Light Threshold Value: {minMaxThresholdValue}");
            Console.WriteLine($"\tMin/max Temp Threshold Value: {minMaxTempThresholdValue}");
            Console.WriteLine($"\tTime to Monitor: {timeToMonitor}");

            Console.WriteLine("\tPress any key to set the alarm");
            Console.CursorVisible = false;
            Console.ReadKey();

            bool thresholdExceeded = false;
            bool thresholdExceededTemperature = false;
            int seconds = 1;

            do
            {
                Console.SetCursorPosition(10, 10);
                Console.WriteLine($"\tTime: {seconds++}");
                thresholdExceeded = AlarmSystemThresholdExceeded(finchRobot, sensorsToMonitor, rangeType, minMaxThresholdValue);
                thresholdExceededTemperature = AlarmSystemTemperatureExceeded(finchRobot, rangeType, minMaxTempThresholdValue);
                finchRobot.wait(1000);

            } while (!thresholdExceeded && !thresholdExceededTemperature && seconds <= timeToMonitor);

            if (thresholdExceeded || thresholdExceededTemperature)
            {
                Console.WriteLine("\tThreshold Exceeded");
                finchRobot.noteOn(600);
                finchRobot.setLED(255, 0, 0);
                finchRobot.wait(500);
                finchRobot.noteOn(900);
                finchRobot.setLED(100, 0, 100);
                finchRobot.wait(500);
                finchRobot.noteOn(600);
                finchRobot.setLED(255, 0, 0);
                finchRobot.wait(500);
                finchRobot.noteOn(900);
                finchRobot.wait(300);
                finchRobot.setLED(0, 0, 0);
                finchRobot.noteOff();
            }
            else
            {
                Console.WriteLine("\tThreshold Not Exceeded");
            }

            DisplayMenuPrompt("Alarm System Menu");

        }

        /// <summary>
        ///#===========================================#
        ///#      Temperature Threshold Exceeded       #
        ///#===========================================#
        ///
        ///Temperature version of Threshold Exceeded bool. 
        ///
        /// </summary>
        static bool AlarmSystemTemperatureExceeded(Finch finchRobot, string rangeType, int minMaxTempThresholdValue)
        {

            double currentTemperatureSensorValue;
            bool thresholdExceededTemperature = false;
            double finchCelsius;
            double finchFahrenheit;

            finchCelsius = finchRobot.getTemperature();
            finchFahrenheit = (finchCelsius * 9) / 5 + 32;
            currentTemperatureSensorValue = finchFahrenheit;


            if (rangeType == "minimum")
            {
                thresholdExceededTemperature = currentTemperatureSensorValue < minMaxTempThresholdValue;

            }
            else
            {
                thresholdExceededTemperature = currentTemperatureSensorValue > minMaxTempThresholdValue;

            }

            return thresholdExceededTemperature;
        }

        /// <summary>
        ///#=================================================#
        ///#         Set Temperature Threshold Value         #
        ///#=================================================#
        ///
        ///User inputs threshold value based on the previously recorded rangeType value with validation of "rangeType" and 
        ///validation of minMaxTempThresholdValue with a tryparse and loop if incorrect to input valid response. 
        ///This method is the temperature threshold value version. 
        /// </summary>
        static int AlarmSystemSetTempThresholdValue(Finch finchRobot, string rangeType)
        {
            int minMaxTempThresholdValue = 0;
            string minMaxThresholdValueTestString;
            bool userResponse;
            double finchCelsius;
            double finchFahrenheit;

            DisplayScreenHeader("Min/Max Temperature Threshold Value");

            finchCelsius = finchRobot.getTemperature();
            finchFahrenheit = (finchCelsius * 9) / 5 + 32;
            Console.WriteLine($"\tThe temperature in fahrenheit is: {finchFahrenheit}");

            switch (rangeType)
            {
                case "maximum":
                    do
                    {
                        Console.Write($"\n\n\tEnter the {rangeType} threshold value:");
                        minMaxThresholdValueTestString = Console.ReadLine();
                        if (Int32.TryParse(minMaxThresholdValueTestString, out minMaxTempThresholdValue))
                        {
                            userResponse = true;
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine($"\n\tThe Threshold Value you input is: {minMaxTempThresholdValue}");
                        }
                        else
                        {
                            userResponse = false;
                            Console.WriteLine();
                            Console.WriteLine("\n\tThis is not a number. Please enter a valid threshold value.");
                        }
                    } while (!userResponse);

                    break;

                case "minimum":
                    do
                    {
                        Console.Write($"\n\n\tEnter the {rangeType} threshold value:");
                        minMaxThresholdValueTestString = Console.ReadLine();
                        if (Int32.TryParse(minMaxThresholdValueTestString, out minMaxTempThresholdValue))
                        {
                            userResponse = true;
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine($"\n\tThe Threshold Value you input is: {minMaxTempThresholdValue}");
                        }

                        else
                        {
                            userResponse = false;
                            Console.WriteLine();
                            Console.WriteLine("\n\tThis is not a number. Please enter a valid threshold value.");
                        }
                    } while (!userResponse);

                    break;

                default:
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("\tPlease enter a rangeType by selecting the range type submenu before proceeding.\n\n");
                    break;
            }

            DisplayContinuePrompt();

            return minMaxTempThresholdValue;
        }
        /// <summary>
        ///#=========================================================#
        ///#      Sensors To Monitor For Temp Menu Combo Alarm       #
        ///#=========================================================#
        ///
        ///User enters which sensor(s) they are using with an echo back to the user before exiting screen. Added validation.
        ///Returns sensorsToMonitor. This menu is duplicated as it is has to be included for user to input values for
        ///fahrenheit and light alarm combo mode. 
        /// </summary>
        static string AlarmSystemSetSensorsToMonitorTemp()
        {
            DisplayScreenHeader("Set which light sensor(s) to monitor");

            string sensorsToMonitor = "";

            do
            {
                Console.Write("\tEnter Light Sensors to Monitor [left, right, both]:");
                sensorsToMonitor = Console.ReadLine().ToLower();

                switch (sensorsToMonitor)
                {
                    case "left":
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine($"\tYou entered the following light sensors to monitor: {sensorsToMonitor}");
                        break;

                    case "right":
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine($"\tYou entered the following light sensors to monitor: {sensorsToMonitor}");
                        break;

                    case "both":
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine($"\tYou entered the following light sensors to monitor: {sensorsToMonitor}");
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter one of the three valid options displayed.\n\n");
                        break;
                }

            } while (sensorsToMonitor != "left" && sensorsToMonitor != "right" && sensorsToMonitor != "both");

            DisplayContinuePrompt();

            return sensorsToMonitor;

        }

        /// <summary>
        ///#==============================================#
        ///#              Temperature Reading             #
        ///#==============================================#
        ///
        ///User presses a key to begin temperature recording. Added validation.
        ///Returns temperatureReading.
        /// </summary>
        static double TemperatureSensorReading(Finch finchRobot)
        {
            DisplayScreenHeader("Temperature Sensor Input");

            double temperatureReading = -100;
            double finchCelsius;

            Console.WriteLine();
            Console.WriteLine("\tPress any key to begin Finch temperature reading.");
            Console.ReadKey();

            finchCelsius = finchRobot.getTemperature();
            temperatureReading = (finchCelsius * 9) / 5 + 32;

            Console.Clear();
            Console.WriteLine($"\n\n\tThe temperature in fahrenheit is: {temperatureReading}");

            DisplayContinuePrompt();

            return temperatureReading;

        }

        #endregion

        #region FINCH USER PROGRAMMING

        /// <summary>
        ///#======================================================#
        ///#          User Programming Menu Screen                #
        ///#======================================================#
        ///
        ///User decides what submenu they would like to visit in order to program the Finch Robot. 
        /// </summary>
        static void UserProgrammingDisplayMenuScreen(Finch finchRobot)
        {

            DisplayScreenHeader("User Programming Menu Screen");

            bool quitMenu = false;
            string menuChoice;

            (int motorSpeed, int ledBrightness, double waitSeconds) commandParameter = (0, 0, 0);
            List<Command> commands = new List<Command>();

            do
            {
                DisplayScreenHeader("User Programming Menu");

                //
                // get user menu choice
                //

                Console.WriteLine("\ta) Set Command Parameters");
                Console.WriteLine("\tb) Add Commands");
                Console.WriteLine("\tc) View Commands");
                Console.WriteLine("\td) Execute Commands");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        commandParameter = UserProgrammingDisplayGetCommandParameters();
                        break;

                    case "b":
                        UserProgrammingDisplayGetCommands(commands);

                        break;

                    case "c":
                        UserProgrammingDisplayCommands(commands);

                        break;

                    case "d":
                        UserProgrammingDisplayExecuteCommands(finchRobot, commands, commandParameter);

                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitMenu);

            DisplayContinuePrompt();

        }

        /// <summary>
        ///#=================================================#
        ///#        User Programming Execute Commands        #
        ///#=================================================#
        ///
        ///Method in which the Finch Robot executes commands that the user has input from previous methods with some quality of life improvements. 
        ///Text was added to inform the user what command the Finch Robot is performing. Color was added to the commands and temperature to help
        ///distinguish that information as the Finch works. Stopmotors was added at the end of the method as a sort of validation to make sure
        ///the Finch doesn't keep moving after command execution is complete. 
        /// </summary>
        static void UserProgrammingDisplayExecuteCommands(Finch finchRobot, List<Command> commands, (int motorSpeed, int ledBrightness, double waitSeconds) commandParameter)
        {

            DisplayScreenHeader("Execute Commands");

            double temperature;

            Console.WriteLine("\n\tThe Finch Robot is ready to execute the commands.");
            DisplayContinuePrompt();
            Console.WriteLine();

            foreach (Command command in commands)
            {
                Console.Write("\tThe Finch Robot will now execute your command:");
                Console.ForegroundColor = (ConsoleColor.DarkCyan);
                Console.Write($" {command}\n");
                Console.ForegroundColor = (ConsoleColor.Green);

                switch (command)
                {
                    case Command.NONE:
                        Console.WriteLine("Invalid Command");
                        break;

                    case Command.MOVEFORWARD:
                        finchRobot.setMotors(commandParameter.motorSpeed, commandParameter.motorSpeed);
                        break;

                    case Command.MOVEBACKWARD:
                        finchRobot.setMotors(-commandParameter.motorSpeed, -commandParameter.motorSpeed);
                        break;

                    case Command.STOPMOTORS:
                        finchRobot.setMotors(0, 0);
                        break;

                    case Command.WAIT:
                        int waitMilliseconds = (int)(commandParameter.waitSeconds * 1000);
                        finchRobot.wait(waitMilliseconds);
                        break;

                    //Provided a timed/speed override for the turn command. This works better turning the robot instead of relying on the user input wait
                    //command which turns the Finch beyond a left or right in most cases, almost doing a 360 or more. Same goes for motor speed. 255 would
                    //have the Finch doing donuts, not turning. Stop motors is used at the end of the command to complete the turn. 
                    //
                    case Command.TURNRIGHT:
                        finchRobot.setMotors(160, 0);
                        finchRobot.wait(1500);
                        finchRobot.setMotors(0, 0);
                        break;

                    case Command.TURNLEFT:
                        finchRobot.setMotors(0, 160);
                        finchRobot.wait(1500);
                        finchRobot.setMotors(0, 0);
                        break;

                    case Command.LEDON:
                        finchRobot.setLED(commandParameter.ledBrightness, commandParameter.ledBrightness, commandParameter.ledBrightness);
                        break;

                    case Command.LEDOFF:
                        finchRobot.setLED(0, 0, 0);
                        break;

                    case Command.GETTEMPERATURE:
                        temperature = finchRobot.getTemperature();
                        Console.Write($"\n\tThe temperature is");
                        Console.ForegroundColor = (ConsoleColor.Magenta);
                        Console.Write($" {temperature} Celsius.\n");
                        Console.ForegroundColor = (ConsoleColor.Green);
                        break;

                    case Command.MUSICDANCE:
                        finchRobot.setLED(0, 0, 255);
                        finchRobot.wait(2000);
                        finchRobot.setMotors(50, 50);
                        finchRobot.noteOn(880);
                        finchRobot.wait(300);
                        finchRobot.setLED(255, 0, 255);
                        finchRobot.noteOn(1174);
                        finchRobot.wait(300);
                        finchRobot.setLED(0, 0, 255);
                        finchRobot.noteOn(880);
                        finchRobot.setMotors(-50, -50);
                        finchRobot.wait(300);
                        finchRobot.setLED(255, 0, 255);
                        finchRobot.noteOn(1174);
                        finchRobot.wait(300);
                        finchRobot.setMotors(50, 50);
                        finchRobot.setLED(0, 0, 255);
                        finchRobot.noteOn(880);
                        finchRobot.wait(300);
                        finchRobot.setLED(255, 0, 255);
                        finchRobot.noteOn(1174);
                        finchRobot.wait(300);
                        finchRobot.setMotors(-50, -50);
                        finchRobot.setLED(0, 0, 255);
                        finchRobot.noteOn(880);
                        finchRobot.wait(200);
                        finchRobot.noteOn(830);
                        finchRobot.wait(200);
                        finchRobot.setMotors(50, -50);
                        finchRobot.noteOn(880);
                        finchRobot.wait(300);

                        //music staff two
                        finchRobot.noteOn(880);
                        finchRobot.wait(200);
                        finchRobot.noteOn(830);
                        finchRobot.wait(200);
                        finchRobot.noteOn(880);
                        finchRobot.wait(200);
                        finchRobot.noteOn(830);
                        finchRobot.wait(200);
                        finchRobot.noteOn(739);
                        finchRobot.wait(300);
                        finchRobot.noteOn(783);
                        finchRobot.wait(200);
                        finchRobot.noteOn(739);
                        finchRobot.wait(200);
                        finchRobot.noteOn(739);
                        finchRobot.wait(500);
                        finchRobot.noteOn(698);
                        finchRobot.wait(300);
                        finchRobot.setLED(0, 0, 255);
                        finchRobot.setMotors(-50, -50);
                        finchRobot.noteOn(880);
                        finchRobot.wait(300);
                        finchRobot.setLED(255, 0, 255);
                        finchRobot.noteOn(1174);
                        finchRobot.wait(300);
                        finchRobot.setLED(0, 0, 255);
                        finchRobot.noteOn(880);
                        finchRobot.setMotors(50, 50);
                        finchRobot.wait(300);
                        finchRobot.setLED(255, 0, 255);
                        finchRobot.noteOn(1174);
                        finchRobot.wait(300);
                        finchRobot.setLED(0, 0, 255);
                        finchRobot.setMotors(-50, -50);
                        finchRobot.noteOn(880);
                        finchRobot.wait(300);

                        //music staff 3
                        finchRobot.setLED(255, 0, 255);
                        finchRobot.noteOn(1174);
                        finchRobot.wait(300);
                        finchRobot.setLED(0, 0, 255);
                        finchRobot.setMotors(-50, 50);
                        finchRobot.noteOn(880);
                        finchRobot.wait(200);
                        finchRobot.noteOn(830);
                        finchRobot.wait(300);
                        finchRobot.noteOn(880);
                        finchRobot.wait(300);
                        finchRobot.noteOn(783);
                        finchRobot.wait(300);
                        finchRobot.noteOn(783);
                        finchRobot.wait(300);
                        finchRobot.noteOn(739);
                        finchRobot.wait(600);
                        finchRobot.noteOn(783);
                        finchRobot.wait(300);
                        finchRobot.noteOn(1066);
                        finchRobot.wait(300);
                        finchRobot.noteOn(987);
                        finchRobot.wait(300);
                        finchRobot.noteOn(880);
                        finchRobot.wait(300);
                        finchRobot.noteOn(783);

                        //music staff 4
                        finchRobot.setLED(0, 0, 255);
                        finchRobot.noteOn(880);
                        finchRobot.setMotors(50, 50);
                        finchRobot.wait(300);
                        finchRobot.setLED(255, 0, 255);
                        finchRobot.noteOn(1174);
                        finchRobot.wait(300);
                        finchRobot.setLED(0, 0, 255);
                        finchRobot.noteOn(880);
                        finchRobot.setMotors(-50, -50);
                        finchRobot.wait(300);
                        finchRobot.setLED(255, 0, 255);
                        finchRobot.noteOn(1174);
                        finchRobot.wait(300);
                        finchRobot.setLED(0, 0, 255);
                        finchRobot.noteOn(880);
                        finchRobot.setMotors(50, 50);
                        finchRobot.wait(300);
                        finchRobot.setLED(255, 0, 255);
                        finchRobot.noteOn(1174);
                        finchRobot.wait(300);
                        finchRobot.setLED(0, 0, 255);
                        finchRobot.noteOn(880);
                        finchRobot.setMotors(-50, -50);
                        finchRobot.wait(200);
                        finchRobot.noteOn(830);
                        finchRobot.wait(300);
                        finchRobot.noteOn(880);
                        finchRobot.wait(300);
                        finchRobot.setMotors(50, -50);
                        finchRobot.noteOn(1066);
                        finchRobot.wait(300);
                        finchRobot.noteOn(1066);
                        finchRobot.wait(300);
                        finchRobot.setMotors(50, -50);
                        finchRobot.noteOn(880);
                        finchRobot.wait(200);
                        finchRobot.noteOn(830);
                        finchRobot.wait(300);

                        //music staff 5
                        finchRobot.setLED(0, 125, 255);
                        finchRobot.noteOn(698);
                        finchRobot.setMotors(-50, -50);
                        finchRobot.wait(600);
                        finchRobot.setLED(0, 250, 255);
                        finchRobot.noteOn(587);
                        finchRobot.setMotors(50, 50);
                        finchRobot.wait(600);
                        finchRobot.setLED(0, 250, 125);
                        finchRobot.noteOn(587);
                        finchRobot.setMotors(-50, -50);
                        finchRobot.wait(600);
                        finchRobot.setLED(0, 250, 0);
                        finchRobot.noteOn(698);
                        finchRobot.setMotors(50, 50);
                        finchRobot.wait(600);
                        finchRobot.setLED(125, 255, 0);
                        finchRobot.noteOn(880);
                        finchRobot.setMotors(-50, -50);
                        finchRobot.wait(600);
                        finchRobot.setLED(255, 125, 0);
                        finchRobot.noteOn(1046);
                        finchRobot.setMotors(50, 50);
                        finchRobot.wait(600);
                        finchRobot.setLED(255, 0, 0);
                        finchRobot.setMotors(50, -50);
                        finchRobot.noteOn(1300);
                        finchRobot.wait(300);
                        finchRobot.noteOn(1174);
                        finchRobot.wait(300);
                        finchRobot.noteOn(830);
                        finchRobot.wait(300);
                        finchRobot.setMotors(-50, 50);
                        finchRobot.noteOn(880);
                        finchRobot.wait(300);
                        finchRobot.setLED(255, 0, 255);
                        finchRobot.noteOn(698);
                        finchRobot.wait(900);
                        finchRobot.noteOff();
                        finchRobot.setMotors(0, 0);
                        finchRobot.setLED(0, 0, 0);
                        break;

                    case Command.DONE:
                        break;

                    default:
                        break;
                }

            }
            //
            //Turns motors off in case user does not input stopmotors command. 
            //
            finchRobot.setMotors(0, 0);
            DisplayMenuPrompt("User Programming");
        }

        /// <summary>
        ///#====================================================#
        ///#          User Programming Display Commands         #
        ///#====================================================#
        ///
        ///Simple method in which commands entered by user in DisplayGetCommand Method are displayed for the user. 
        /// </summary>
        static void UserProgrammingDisplayCommands(List<Command> commands)
        {
            DisplayScreenHeader("Selected Commands");

            foreach (Command command in commands)
            {
                Console.ForegroundColor = (ConsoleColor.DarkCyan);
                Console.WriteLine("\t\t" + command);
                Console.ForegroundColor = (ConsoleColor.Green);
            }

            DisplayMenuPrompt("User Programming");
        }

        /// <summary>
        ///#================================================#
        ///#     User Programming Display Get Commands      #
        ///#================================================#
        ///
        ///User enters commands from the enum that was created at the top of the program. The first foreach loop displays the commands for the user. 
        ///Next we have validation for commands that user enters to check against the enum commands. 
        /// </summary>
        static void UserProgrammingDisplayGetCommands(List<Command> commands)
        {

            Command command;
            Console.ForegroundColor = (ConsoleColor.Green);
            bool validResponse;
            bool isDoneAddingCommands = false;
            string userResponse;


            DisplayScreenHeader("Enter Commands");
            Console.ForegroundColor = (ConsoleColor.DarkYellow);
            Console.WriteLine("**Please type 'DONE' when finished entering commands.**\n");
            Console.ForegroundColor = (ConsoleColor.Green);

            foreach (Command commandName in Enum.GetValues(typeof(Command)))
            {
                if (commandName.ToString() != "NONE") Console.WriteLine("\t\t" + commandName);
            }
            Console.Write("\n\tEnter one of the commands above:");

            do
            {
                validResponse = true;
                Console.CursorVisible = true;
                //Console.Write("\n\tEnter one of the commands above:");
                userResponse = Console.ReadLine().ToUpper();
                Console.CursorVisible = false;


                if (userResponse != "DONE")
                {
                    //
                    //user entered invalid command
                    //
                    if (!Enum.TryParse(userResponse, out command) || int.TryParse(userResponse, out int number))
                    {
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a proper command.");
                        DisplayContinuePrompt();
                        validResponse = false;
                    }
                    //
                    //user entered valid command
                    //
                    else
                    {
                        commands.Add(command);
                        Console.WriteLine();
                        Console.Write("\tYou Entered");
                        Console.ForegroundColor = (ConsoleColor.DarkCyan);
                        Console.Write($" { userResponse}.");
                        Console.ForegroundColor = (ConsoleColor.Green);
                        Console.Write(" Please add another command: ");
                    }

                }
                else
                {
                    isDoneAddingCommands = true;
                }

            } while (!validResponse || !isDoneAddingCommands);

            DisplayMenuPrompt("User Programming");
        }



        /// <summary>
        ///#===================================================#
        ///#     User Programming Get Command Parameters       #
        ///#===================================================#
        ///
        ///Collects command parameters from user for LED brightness, Motor Speed, and wait time with validation of input. (Integer and range of 0-255)
        /// </summary>
        static (int motorSpeed, int ledBrightness, double waitSeconds) UserProgrammingDisplayGetCommandParameters()

        {
            (int motorSpeed, int ledBrightness, double waitSeconds) commandParameter;

            commandParameter = (0, 0, 0);
            bool userResponse = false;


            //validation for motor speed, first to verify input is an integer, and second within the range of 0-255. Echo input to user.
            //
            do
            {
                DisplayScreenHeader("Get Command Parameters");
                Console.CursorVisible = true;
                Console.Write("\tEnter Motor Speed from 0 up to 255.(Higher is faster):");
                if (int.TryParse(Console.ReadLine(), out commandParameter.motorSpeed))
                {
                    if (commandParameter.motorSpeed <= 255 && commandParameter.motorSpeed >= 0)
                    {
                        userResponse = true;
                        Console.CursorVisible = false;
                        Console.Clear();
                        Console.WriteLine($"\n\n\tYou entered {commandParameter.motorSpeed}");
                        DisplayContinuePrompt();

                    }
                    else
                    {
                        userResponse = false;
                        Console.CursorVisible = false;
                        Console.Clear();
                        Console.WriteLine("\n\n\tPlease enter a valid number.");
                        DisplayContinuePrompt();
                    }
                }
                else
                {
                    userResponse = false;
                    Console.CursorVisible = false;
                    Console.Clear();
                    Console.WriteLine("\n\n\tPlease enter a valid number.");
                    DisplayContinuePrompt();
                }

            } while (!userResponse);

            //validation for LED brightness - first to verify input is an integer, and second to verify within the range of 0-255. Echo input.
            //
            do
            {
                DisplayScreenHeader("Get Command Parameters");
                Console.CursorVisible = true;
                Console.Write("\tEnter LED Brightness up to 255 - higher numbers are brighter:");
                if (int.TryParse(Console.ReadLine(), out commandParameter.ledBrightness))
                {
                    if (commandParameter.ledBrightness <= 255 && commandParameter.ledBrightness >= 0)
                    {
                        userResponse = true;
                        Console.CursorVisible = false;
                        Console.Clear();
                        Console.WriteLine($"\n\n\tYou entered {commandParameter.ledBrightness}");
                        DisplayContinuePrompt();
                    }
                    else
                    {
                        userResponse = false;
                        Console.CursorVisible = false;
                        Console.Clear();
                        Console.WriteLine("\n\n\tPlease enter a valid number.");
                    }
                }
                else
                {
                    userResponse = false;
                    Console.CursorVisible = false;
                    Console.Clear();
                    Console.WriteLine("\n\n\tPlease enter a valid number.");
                }

            } while (!userResponse);

            //validation for wait time to verify that user entered a number. Echo to user. 
            //
            do
            {
                DisplayScreenHeader("Get Command Parameters");

                Console.Write("\tEnter Wait Time [seconds]:");
                Console.CursorVisible = true;
                if (double.TryParse(Console.ReadLine(), out commandParameter.waitSeconds))
                {
                    userResponse = true;
                    Console.CursorVisible = false;
                    Console.Clear();
                    Console.WriteLine($"\n\n\tYou entered {commandParameter.waitSeconds}");
                    DisplayContinuePrompt();
                }
                else
                {
                    userResponse = false;
                    Console.CursorVisible = false;
                    Console.Clear();
                    Console.WriteLine("\n\n\tYou have not entered a number. Please try again.");
                    DisplayContinuePrompt();
                }

            } while (!userResponse);

            return commandParameter;
        }

        #endregion

        #region FINCH ROBOT MANAGEMENT

        /// <summary>
        /// #===================================================#
        /// #           Disconnect the Finch Robot              #
        /// #===================================================#
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("\tAbout to disconnect from the Finch robot.");
            DisplayContinuePrompt();
            finchRobot.setLED(255, 150, 00);
            finchRobot.noteOn(1047);
            finchRobot.wait(300);
            finchRobot.noteOff();
            finchRobot.wait(50);
            finchRobot.noteOn(784);
            finchRobot.wait(300);
            finchRobot.noteOff();
            finchRobot.wait(50);
            finchRobot.noteOn(659);
            finchRobot.wait(600);
            finchRobot.noteOff();
            finchRobot.wait(1000);
            finchRobot.disConnect();

            Console.WriteLine("\tThe Finch robot is now disconnected.");


            DisplayContinuePrompt();
        }

        /// <summary>
        /// #===================================================#
        /// #             Connect the Finch Robot               #
        /// #===================================================#
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>notify if the robot is connected</returns>
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            DisplayScreenHeader("Please connect Finch Robot");

            Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
            DisplayContinuePrompt();

            robotConnected = finchRobot.connect();

            //SCHOOLCLASS
            //if (robotConnected)
            //{
            //    Console.WriteLine("\tRobot Connect.");
            //    finchRobot.setLED(0, 255, 0);
            //    finchRobot.noteOn(261);

            //}
            //else
            //{
            //    Console.WriteLine("There was a problem connecting to finsh robot");
            //}

            // Connection test and user feedback - connection chirp, green LED, and connection success message. 
            while (robotConnected)
            {
                finchRobot.setLED(00, 30, 00);
                finchRobot.noteOn(784);
                finchRobot.wait(300);
                finchRobot.noteOff();
                finchRobot.wait(50);
                finchRobot.noteOn(784);
                finchRobot.wait(300);
                finchRobot.noteOff();
                finchRobot.wait(50);
                finchRobot.noteOn(1047);
                finchRobot.wait(600);
                finchRobot.noteOff();
                finchRobot.setLED(00, 00, 00);

                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("\tFinch robot connected successfully.");
                //DisplayContinuePrompt();
                DisplayMenuPrompt("Main Menu");
                return robotConnected;
            }

            //
            // If user unable to connect, display try again message. LED off, sound off, back to main menu. 
            //
            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();
            Console.WriteLine("\tFinch robot not detected. Please check connection and try again.");
            DisplayMenuPrompt("Main Menu");
            return robotConnected;
        }

        #endregion

        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion

    }


}
