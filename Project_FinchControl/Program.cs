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
    //  Description: Sprint two in the Finch series of missions. 
    //               Data Recorder - Validation, methods,  
    //               light, and temperature. Tables included that
    //               display array data and temp conversion.
    //  Application Type: Console
    //  Author: Hill, Shane
    //  Dated Created: 9/28/2020
    //  Last Modified: 10/11/2020
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
        /// <returns>notify if the robot is connected</returns>
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
        //}

        /// <summary>
        /// #=======================================================================#
        /// #          Data Recorder > Get the Frequency of Data Points             #
        /// #=======================================================================#
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>notify if the robot is connected</returns>
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
        /// <returns>notify if the robot is connected</returns>
        static double[] DataRecorderDisplayGetDataSet(int numberOfDataPoints, double frequencyOfDataPointsSeconds, Finch finchRobot)
        {
            double[] temperatures = new double[numberOfDataPoints];

            DisplayScreenHeader("Get Temperature Data Set");

            Console.WriteLine($"\tNumber of Data Points: {numberOfDataPoints}");
            Console.WriteLine($"\tFrequency of Data Points {frequencyOfDataPointsSeconds}");
            Console.WriteLine();
            Console.WriteLine("The Finch Robot is ready to record the temperature data. Press any key to begin.");
            Console.ReadKey();

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
        /// <returns>notify if the robot is connected</returns>
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
        /// <returns>notify if the robot is connected</returns>
        static void DataRecorderDisplayGetDataSet(double[] temperatures, double[] lightAverage)
        {
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
        /// <returns>notify if the robot is connected</returns>
        static void ConvertCelsiusToFahrenheit(double[] temperatures)
        {

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
        /// <returns>notify if the robot is connected</returns>
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
        ///Under Development 
        /// </summary>
        static void AlarmSystemDisplayMenuScreen(Finch finchRobot)
        {

            DisplayScreenHeader("Alarm System Menu Screen");


            Console.WriteLine("\tThis module is under development");

            DisplayContinuePrompt();

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
