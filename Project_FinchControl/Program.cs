using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Remoting.Messaging;
using FinchAPI;

namespace Project_FinchControl
{

    // #==========================================================#
    // 
    //  Title: Finch Control
    //  Description: Sprint three in the Finch series of missions. 
    //               Alarm System - Validation, methods,  
    //               light, and temperature submenu added for 
    //               light only alarm, temp only alarm, and
    //               temp/light combo alarm. 
    //  Application Type: Console
    //  Author: Hill, Shane
    //  Dated Created: 9/28/2020
    //  Last Modified: 10/18/2020
    // 
    // #==========================================================#

    class Program
    {
        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SetTheme();

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
        ///User selects which submenu they'd like to access for the alarm system. The use rmust go through all pertinet subnets before allowed access
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
                        if (temperatureReading == -100 || rangeType == "" || MinMaxTempThresholdValue == 0 || timeToMonitor == 0 )
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
            int timeToMonitor,int minMaxTempThresholdValue)
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
                thresholdExceededTemperature = AlarmSystemTemperatureExceeded(finchRobot,rangeType, minMaxTempThresholdValue);
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
        ///Under Development 
        /// </summary>
        static void UserProgrammingDisplayMenuScreen(Finch finchRobot)
        {

            DisplayScreenHeader("User Programming Menu Screen");


            Console.WriteLine("\tThis module is under development");

            DisplayContinuePrompt();

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
