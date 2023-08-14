using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace FInal_Project
{
    internal class Program
    {
        public class Song
        {
            public string Title { get; set; }
            public string Artist { get; set; }
            public DateTime LastDatePlayed { get; set; }
        }

        public class SongLibrary
        {
            public List<Song> AllSongs { get; set; } = new List<Song>();
            public int LibraryLength { get { return AllSongs.Count(); } }

            public void AddingSongToLibrary(List<Song> IncomingSong)
            {
                AllSongs.Add(IncomingSong[0]);
                Console.WriteLine("Successfully added " + IncomingSong[0].Title + ", by " + IncomingSong[0].Artist + " to your library");
            }

            public bool CheckIfSongExists(String IncomingSong)
            {
                bool Status = false;

                foreach (Song i in AllSongs)
                {
                    if (IncomingSong == i.Title)
                    {
                        Status = true;
                        return Status;
                    }
                }

                return Status;
            }

            public void DisplaySongLibrary(string IncomingUserInput)
            {
                if (IncomingUserInput == "ALLSONGS")
                {
                    Console.WriteLine("");

                    foreach (Song i in AllSongs)
                    {
                        Console.WriteLine(i.Title + ", by " + i.Artist);
                    }
                }

                if (IncomingUserInput == "ALLSONGSBYANARTIST")
                {
                    Console.WriteLine("Enter an Artist");
                    string ArtistName = Console.ReadLine();
                    Console.WriteLine("");
                    string PotentialArtist = "";

                    foreach (Song i in AllSongs)
                    {
                        if (i.Artist == ArtistName)
                        {
                            Console.WriteLine(i.Title + ", by " + i.Artist);
                            PotentialArtist = i.Artist;
                        }
                    }

                    if (PotentialArtist == "")
                    {
                        Console.WriteLine("No artists found under that name. Redirecting you");
                    }
                }

                Console.WriteLine("");
            }

            public void OpenSongTabFile(string IncomingSongName)
            {
                using Process fileopener = new Process();
                fileopener.StartInfo.FileName = "explorer";
                fileopener.StartInfo.Arguments = "\"" + @"C:\Users\Tom\OneDrive\Desktop\Guitar Files\Song Tabs\" + IncomingSongName + ".png" + "\"";
                fileopener.Start();
            }

            public void OpenSongSoundFile(string IncomingSongName)
            {
                using Process fileopener = new Process();
                fileopener.StartInfo.FileName = "explorer";
                fileopener.StartInfo.Arguments = "\"" + @"C:\Users\Tom\OneDrive\Desktop\Guitar Files\Song Sound Bites\" + IncomingSongName + ".mp3" + "\"";
                fileopener.Start();
            }

            public void Metronome()
            {
                Console.WriteLine("Enter Desired BPM");
                int BPM = Convert.ToInt32(Console.ReadLine());
                int Conversion = (60000 / BPM) - 100;
                Console.WriteLine("To stop the metronome, press any key on your keyboard!");

                while (!Console.KeyAvailable)
                {
                    Console.Beep(300, 100);
                    Thread.Sleep(Conversion);
                }
            }

            public string OldestSong()
            {
                DateTime OldestSongDate = AllSongs[0].LastDatePlayed;
                string OldestSongTitle = AllSongs[0].Title;

                foreach (Song i in AllSongs)
                {
                    if (i.LastDatePlayed == DateTime.MinValue)
                    {
                        return i.Title;
                    }

                    if (i.LastDatePlayed < OldestSongDate)
                    {
                        OldestSongDate = i.LastDatePlayed;
                        OldestSongTitle = i.Title;
                    }
                }

                return OldestSongTitle;
            }

            public void SongOptions(string IncomingSong)
            {
                foreach (Song i in AllSongs)
                {
                    if (IncomingSong == i.Title)
                    {
                        i.LastDatePlayed = DateTime.Now;
                    }
                }

                bool Status = false;

                while (Status == false)
                {
                    Console.WriteLine("Would you like to Open The Tabs, Hear The Song, Use A Metronome, Or Finish Up");
                    string UserInput = Console.ReadLine().ToUpper().Replace(" ", "");
                    List<string> Options = new List<string> { "OPENTHETABS", "HEARTHESONG", "USEAMETRONOME", "FINISHUP" };

                    if (UserInput == "OPENTHETABS")
                    {
                        OpenSongTabFile(IncomingSong);
                    }

                    if (UserInput == "HEARTHESONG")
                    {
                        OpenSongSoundFile(IncomingSong);
                    }

                    if (UserInput == "USEAMETRONOME")
                    {
                        Metronome();
                    }

                    if (UserInput == "FINISHUP")
                    {
                        Status = true;
                        Console.WriteLine("Hope you enjoyed playing " + IncomingSong + "!");
                    }

                    if (Options.Contains(UserInput) == false)
                    {
                        Console.WriteLine("Invalid Input");
                    }
                }
            }

            public string MostRecent()
            {
                DateTime MostRecentDate = AllSongs[0].LastDatePlayed;
                string MostRecentTitle = AllSongs[0].Title;

                foreach (Song i in AllSongs)
                {
                    if (i.LastDatePlayed > MostRecentDate)
                    {
                        MostRecentDate = i.LastDatePlayed;
                        MostRecentTitle = i.Title;
                    }
                }

                return MostRecentTitle;
            }
        }

        //----------------------------------------------------------------------------------------------------------

        public class Session
        {
            public int Time { get; set; }
        }

        public class GuitarData
        {
            public List<Session> AllSessions { get; set; } = new List<Session>();

            public void AddSession(List<Session> IncomingSession)
            {
                AllSessions.Add(IncomingSession[0]);
            }

            public void DisplayTimeRemaining()
            {
                int TotalTime = 0;

                foreach (Session i in AllSessions)
                {
                    TotalTime = TotalTime + i.Time;
                }

                int RemainingTimeInMinutes = 6000 - TotalTime;
                double RemainingTimeInHours = RemainingTimeInMinutes / 60.0;
                Console.WriteLine("");
                Console.WriteLine("You have " + RemainingTimeInMinutes + " minutes, or " + RemainingTimeInHours + " hours of playing time left until you need to change your strings!");
                Console.WriteLine("");
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------

        public static void Main(string[] args)
        {
            string[] UserInputOptions = { "Play Most Recently Played Song"," Play An Existing Song", " Play A Long Lost Song", " Add A Song", " Tune Guitar By Ear", " View Guitar Data", " Use A Metronome", " Exit" };
            string CombinedInputOptions = string.Join(",", UserInputOptions);

            for (int i = 0; i < UserInputOptions.Length; i++)
            {
                UserInputOptions[i] = UserInputOptions[i].ToUpper();
                UserInputOptions[i] = UserInputOptions[i].Replace(" ", "");
            }

            SongLibrary Library = new SongLibrary();
            GuitarData Data = new GuitarData();
            bool MainProgramStatus = true;
            string dataFile = args[0];
            string dataFile2 = args[1];

            // If library Data File Exists
            if (File.Exists(dataFile))
            {
                string SavedData = File.ReadAllText(dataFile);
                Library.AllSongs = JsonSerializer.Deserialize<List<Song>>(SavedData);
            }

            // If Guitar Data File Exists
            if (File.Exists(dataFile2))
            {
                string SavedGuitarData = File.ReadAllText(dataFile2);
                Data.AllSessions = JsonSerializer.Deserialize<List<Session>>(SavedGuitarData);
            }

            Console.WriteLine("Welcome Back!");

            while (MainProgramStatus == true)
            {
                Console.WriteLine("What Would You Like To Do: " + CombinedInputOptions);
                string Response = Console.ReadLine();
                Response = Response.ToUpper();
                Response = Response.Replace(" ", "");

                //play most recently played song
                if (Response == UserInputOptions[0])
                {
                    string MostRecentlyPlayed = Library.MostRecent();
                    Library.SongOptions(MostRecentlyPlayed);
                }

                //Play an Existing Song
                if (Response == UserInputOptions[1])
                {
                    bool Status = false;
                    string SelectedSong = "";

                    Console.WriteLine("Would you like to see the songs in your library? (Y/N)");
                    string YesOrNo = Console.ReadLine().ToUpper().Replace(" ","");

                    if (YesOrNo == "Y" || YesOrNo == "YES")
                    {
                        bool Status2 = false;
                        while (Status2 == false)
                        {
                            Console.WriteLine("All Songs, or All Songs By An Artist");
                            string UserInput = Console.ReadLine().ToUpper().Replace(" ", "");
                            List<string> Options = new List<string> { "ALLSONGS", "ALLSONGSBYANARTIST"};

                            if (Options.Contains(UserInput) == true)
                            {
                                Library.DisplaySongLibrary(UserInput);
                                Status2 = true;
                            }

                            if (Options.Contains(UserInput) == false)
                            {
                                Console.WriteLine("Invalid Input");
                            }
                        }
                    }

                    while (Status == false)
                    {
                        Console.WriteLine("What song in your library would you like to play");
                        SelectedSong = Console.ReadLine();
                        Status = Library.CheckIfSongExists(SelectedSong);

                        if (Status == false)
                        {
                            Console.WriteLine("I'm sorry, that song is not in your library. Please try again");
                        }
                    }

                    Status = false;
                    Console.WriteLine(SelectedSong + " is a great choice!");
                    Library.SongOptions(SelectedSong);

                }

                // Play a song that hasnt been played in a while
                if (Response == UserInputOptions[2])
                {
                    string OldestSongTitle = Library.OldestSong();
                    Library.SongOptions(OldestSongTitle);
                }

                //Add a Song
                if (Response == UserInputOptions[3])
                {
                    List<Song> MovingSong = new List<Song>();
                    Song NewSong = new Song();
                    bool Status = false;

                    while (Status == false)
                    {
                        Console.WriteLine("What is the title? Please Make sure spacing and capitalization is correct!");
                        NewSong.Title = Console.ReadLine().Trim();
                        if (NewSong.Title == "")
                        {
                            Console.WriteLine("Invalid Input, please enter a song title");
                        }
                        
                        else
                        {
                            Status = true;
                        }
                    }

                    Status = false;

                    while (Status == false)
                    {
                        Console.WriteLine("What is the Artist? Please Make sure spacing and capitalization is correct!");
                        NewSong.Artist = Console.ReadLine().Trim();
                        if (NewSong.Artist == "")
                        {
                            Console.WriteLine("Invalid Input, please enter an Artist");
                        }

                        else
                        {
                            Status = true;
                        }
                    }

                    NewSong.LastDatePlayed = DateTime.MinValue;
                    MovingSong.Add(NewSong);

                    Console.WriteLine("Have you added the Tabs and Audio files to the files folder? And, do the Tabs and Song files titles match the song title? ('yes' or 'no')");
                    string FilesAddedAnswer = Console.ReadLine().ToUpper().Replace(" ","");

                    if (FilesAddedAnswer != "YES")
                    {
                        Console.WriteLine("Please add the Tabs and Song files to the correct folders. Otherwise you will not be able to play this song!");
                    }

                    else
                    {
                        Console.WriteLine("Glad you thought ahead!");
                    }

                    Library.AddingSongToLibrary(MovingSong);
                }
                
                //Tune Guitar
                //This is what I use irl
                if (Response == UserInputOptions[4])
                {
                    Process.Start(new ProcessStartInfo("https://www.youtube.com/watch?v=DxkMQvmKZaM&ab_channel=187Guitarplayer") { UseShellExecute = true });
                }

                //View Guitar Data
                if (Response == UserInputOptions[5])
                {
                    Data.DisplayTimeRemaining();
                }

                //Metronome
                if (Response == UserInputOptions[6])
                {
                    Library.Metronome();
                }

                //Exit
                if (Response == UserInputOptions[7])
                {
                    List<Session> MovingSession = new List<Session>();
                    Session NewSession = new Session();
                    string SerializedLibrary = JsonSerializer.Serialize(Library.AllSongs);
                    File.WriteAllText(dataFile, SerializedLibrary);

                    Console.WriteLine("How long was this session? (In Minutes)");
                    NewSession.Time = Convert.ToInt32(Console.ReadLine());
                    MovingSession.Add(NewSession);
                    Data.AddSession(MovingSession);
                    Data.DisplayTimeRemaining();

                    string SerializedGuitarData = JsonSerializer.Serialize(Data.AllSessions);
                    File.WriteAllText(dataFile2, SerializedGuitarData);

                    System.Environment.Exit(1);
                }

                if (UserInputOptions.Contains(Response) == false)
                {
                    Console.WriteLine("Invalid Input");
                }
            }
        }
    }
}