﻿using LastSliceUWP.Models;
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
                    DateTime startTime = problem.TimeIssued;
                    Debug.WriteLine("Issued Time = {0}", startTime);
                    //Debug.WriteLine("Start Time = {0}", DateTime.Now);

                    ResultText.Text = puzzle;
                    
                    int maxLength = 0;
                    foreach(string s in problem.Puzzle["Lines"])
                        if (s.Length > maxLength)
                            maxLength = s.Length;

                    char[][] grid = new char[problem.Puzzle["Lines"].Length][];

                    for(int i = 0; i < problem.Puzzle["Lines"].Length; i++)
                    {
                        grid[i] = new char[maxLength];
                        for (int j = 0; j < 20; j++)
                        {
                            if (j < problem.Puzzle["Lines"][i].Length)
                            {
                                grid[i][j] = problem.Puzzle["Lines"][i][j];
                            }
                            else
                            {
                                grid[i][j] = ' ';
                            }
                        }
                    }

                    Solver solver = new Solver();
                    //Debug.WriteLine("*****************");

                    Solution soln = new Solution();
                    soln.PuzzleId = problem.Id;
                    soln.Initials = "VAK";
                    soln.Words = solver.findWords(grid, problem.Puzzle["Lines"].Length, maxLength);
                    //Debug.WriteLine("*****************");
                    //Word w = new Word();
                    //w.word = problem.Puzzle["Lines"][0];
                    //w.x = 0;
                    //w.y = 0;
                    //w.direction = "EEEE";
                    //soln.Words = new Word[] { w };

                    // TODO: Now show your Swagger and find the solution.

                    Debug.WriteLine("Time taken = {0}", DateTime.Now - startTime);
                    string solution = JsonConvert.SerializeObject(soln);
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
