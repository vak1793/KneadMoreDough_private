using LastSliceUWP.Models;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LastSliceUWP
{
    public sealed partial class MainPage : Page
    {
        private ChallengeService challengeService = new ChallengeService();

        public MainPage()
        {
            InitializeComponent();
            ShowUserCache();
        }

        private void ShowUserCache()
        {
            bool userHasLoggedIn = challengeService.HasUserLoggedIn();

            if (userHasLoggedIn)
            {
                currentUserText.Text = "Cached User in Storage";
            }
            else
            {
                currentUserText.Text = "No Cached User";
            }
        }

        private void btnClearUserCacheClick(object sender, RoutedEventArgs e)
        {
            challengeService.ClearUserCache();
            ShowUserCache();
        }

        private async void btnChallenge2Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string token = await challengeService.Login();

                if (!string.IsNullOrEmpty(token))
                {
                    string puzzle = await challengeService.GetPuzzle();
                    Problem problem = JsonConvert.DeserializeObject<Problem>(puzzle);
                    ResultText.Text = puzzle;
                    string[] ingredients = new string[] {
                        "ANCHOVY",
                        "BACON",
                        "CHEESE",
                        "GARLIC",
                        "GREENPEPPER",
                        "HABANERO",
                        "JALAPENO",
                        "MUSHROOM",
                        "OLIVE",
                        "ONION",
                        "PINEAPPLE",
                        "PEPPERONI",
                        "SAUSAGE",
                        "TOMATOES"
                    };

                    string[] shortIngredients = new string[] {
                        "AN",
                        "BA",
                        "CH",
                        "GA",
                        "GB",
                        "HB",
                        "JP",
                        "MR",
                        "OL",
                        "ON",
                        "PA",
                        "PP",
                        "SA",
                        "TM"
                    };

                    Solver solver = new Solver();
                    Debug.WriteLine("*****************");
                    solver.findWords();
                    Debug.WriteLine("*****************");
                    //for(int i = 0; i < 15; i++) {
                    //    Console.WriteLine(problem.Puzzle["Values"][i]);
                    //}

                    Solution soln = new Solution();
                    soln.PuzzleId = problem.Id;
                    soln.Initials = "V.A.K";
                    Word w = new Word();
                    w.word = problem.Puzzle["Lines"][0];
                    w.x = 0;
                    w.y = 0;
                    w.direction = "EEEE";
                    soln.Words = new Word[] { w };
                    
                    // TODO: Now show your Swagger and find the solution.

                    string solution = JsonConvert.SerializeObject(soln); ;
                    string solutionResponse = await challengeService.PostSolutionToPuzzle(solution);

                    ResultText.Text = solutionResponse;

                    // TODO: Check the solution response to see if you got the correct solution
                }
            }
            catch (Exception ex)
            {
                string exceptionDetails = ex.ToString();
                ShowException(exceptionDetails);
            }

            ShowUserCache();
        }

        private async void ShowException(string message)
        {
            MessageDialog dialog = new MessageDialog(message, "Sorry, an error occurred.");
            await dialog.ShowAsync();
        }
    }
}
