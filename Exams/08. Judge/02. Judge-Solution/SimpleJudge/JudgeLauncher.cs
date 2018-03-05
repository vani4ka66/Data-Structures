using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public static class JudgeLauncher 
{
    public static void Main()
    {
        Judge judge = new Judge();

        Dictionary<int, Submission> submissions = new Dictionary<int, Submission>();

        for (int i = 0; i < 30000; i++)
        {
            int submissionId = i+1;
            int userId = i+1;
            SubmissionType type = (SubmissionType.CSharpCode);
            int contestId = i+1;
            int points = i+1;

            Submission submission = new Submission(submissionId, points, type, contestId, userId);

            if (!submissions.ContainsKey(submissionId))
            {
                submissions.Add(submissionId, submission);
            }


            judge.AddContest(contestId);
            judge.AddUser(userId);
            judge.AddSubmission(submission);
        }

        var expected = submissions.Values.Where(x => x.UserId == 3).GroupBy(x => x.ContestId).Select(x => x.OrderByDescending(s => s.Points).ThenBy(s => s.Id).First()).OrderByDescending(x => x.Points).ThenBy(x => x.Id).Select(x => x.ContestId);
        var sw = Stopwatch.StartNew();
        IEnumerable<int> result = judge.ContestsByUserIdOrderedByPointsDescThenBySubmissionId(3);
        sw.Stop();

        //Assert.Less(sw.ElapsedMilliseconds, 200);
        //CollectionAssert.AreEqual(expected, result);
        Console.WriteLine(sw.ElapsedMilliseconds);

        foreach (var submission in expected)
        {
            Console.WriteLine(submission);
        }

        Console.WriteLine("------");

        foreach (var submission in result)
        {
            Console.WriteLine(submission);
        }

    }

}


