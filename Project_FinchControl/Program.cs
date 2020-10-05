using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;

namespace Project_FinchControl
{

    // #==========================================================#
    // 
    //  Title: Finch Control 
    //  Description: Sprint one in the Finch series of missions. 
    //               Menu, continue, opening, closing, and 
    //               validation screens. Methods for the rest
    //               of the project started. Talent show complete.
    //  Application Type: Console
    //  Author: Hill, Shane
    //  Dated Created: 9/28/2020
    //  Last Modified: 10/2/2020
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

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Connect Finch Robot");
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
                Console.WriteLine("\t**This module is under development.**\n\n");
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

            Console.WriteLine("\tThis module is under development");
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

                Console.WriteLine("\tFinch robot connected successfully.");
                DisplayContinuePrompt();
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
