using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Judge : IJudge
{
    private SortedDictionary<int, int> usersIds;
    private SortedDictionary<int, int> contestIds;
    private OrderedDictionary<int, Submission> submisionsIds;
    private Dictionary<SubmissionType, Bag<int>> bySubmitionType;
    private Dictionary<string, OrderedBag<Submission>> byContentUserPoints;
    private Dictionary<SubmissionType, OrderedBag<Submission>> bySubmitionTypePoints;
    private OrderedBag<Submission> submits;
    //private Dictionary<int, List<Submission>> byIdSubmitions;



    public Judge()
    {
        usersIds = new SortedDictionary<int, int>();
        contestIds = new SortedDictionary<int, int>();
        submisionsIds = new OrderedDictionary<int, Submission>();
        bySubmitionType = new Dictionary<SubmissionType, Bag<int>>();
        byContentUserPoints = new Dictionary<string, OrderedBag<Submission>>();
        bySubmitionTypePoints = new Dictionary<SubmissionType, OrderedBag<Submission>>();
        submits = new OrderedBag<Submission>();
        //byIdSubmitions = new Dictionary<int, List<Submission>>();
    }

    public void AddContest(int contestId)
    {
        contestIds[contestId] = contestId;
    }

    public void AddSubmission(Submission submission)
    {
        if (!usersIds.ContainsKey(submission.UserId))
        {
            throw new InvalidOperationException();
        }
        if (!contestIds.ContainsKey(submission.ContestId))
        {
            throw new InvalidOperationException();
        }

        //usersIds[submission.UserId] = submission.UserId;
        //contestIds[submission.ContestId] = submission.ContestId;
        submisionsIds[submission.Id] = submission;

        if (!bySubmitionType.ContainsKey(submission.Type))
        {
            bySubmitionType.Add(submission.Type, new Bag<int>());
            bySubmitionTypePoints.Add(submission.Type, new OrderedBag<Submission>());
        }
        bySubmitionType[submission.Type].Add(submission.ContestId);
        bySubmitionTypePoints[submission.Type].Add(submission);

        var key = submission.ContestId + ""+ submission.UserId +"" +  submission.Points;
        if (!byContentUserPoints.ContainsKey(key))
        {
            byContentUserPoints.Add(key, new OrderedBag<Submission>());
        }
        byContentUserPoints[key].Add(submission);
        submits.Add(submission);

        /*if (!byIdSubmitions.ContainsKey(submission.UserId))
        {
            byIdSubmitions.Add(submission.UserId, new List<Submission>());
        }
        byIdSubmitions[submission.UserId].Add(submission);*/
    }

    public void AddUser(int userId)
    {
        usersIds[userId] = userId;
    }

    public void DeleteSubmission(int submissionId)
    {
        if (!submisionsIds.ContainsKey(submissionId))
        {
            throw new InvalidOperationException();
        }
        var val = submisionsIds[submissionId];

        submisionsIds.Remove(submissionId);
        bySubmitionTypePoints[val.Type].Remove(val);
        byContentUserPoints[val.ContestId + "" + val.UserId + val.Points].Remove(val);
        submits.Remove(val);

        /*int idUser = 0;
        foreach (var byIdSubmition in byIdSubmitions)
        {
            foreach (var idSubmition in byIdSubmition.Value)
            {
                if (idSubmition.Id.Equals(submissionId))
                {
                    idUser = byIdSubmition.Key;
                    return;
                }
            }
        }

        byIdSubmitions[idUser].Remove(val);*/
    }

    public IEnumerable<Submission> GetSubmissions()
    {
        foreach (var submisionsId in submisionsIds)//.Reverse())
        {
            yield return submisionsId.Value;
        }
    }

    public IEnumerable<int> GetUsers()
    {
        foreach (var usersId in usersIds)
        {
            yield return usersId.Key;
        }
    }

    public IEnumerable<int> GetContests()
    {
        foreach (var contestId in contestIds)
        {
            yield return contestId.Key;
        }
    }

    public IEnumerable<Submission> SubmissionsWithPointsInRangeBySubmissionType(int minPoints, int maxPoints, SubmissionType submissionType)
    {
        foreach (var i in bySubmitionTypePoints[submissionType])
        {
            if (i.Points >= minPoints && i.Points <= maxPoints)
            {
                yield return i;
            }
        }
    }

    public IEnumerable<int> ContestsByUserIdOrderedByPointsDescThenBySubmissionId(int userId)
    {
        HashSet<int> set = new HashSet<int>();
        var result = submits.Where(x => x.UserId.Equals(userId));
        foreach (var submission in result)
        {
            set.Add(submission.ContestId);
        }
        return set;

        /*var result = byIdSubmitions[userId];

        foreach (var submission in result)
        {
            yield return submission.ContestId;
        }*/
    }

    public IEnumerable<Submission> SubmissionsInContestIdByUserIdWithPoints(int points, int contestId, int userId)
    {
        if (!usersIds.ContainsKey(userId))
        {
            throw new InvalidOperationException();
        }

        if (!contestIds.ContainsKey(contestId))
        {
            throw new InvalidOperationException();
        }
        var key = contestId + "" + userId + points;

        if (!byContentUserPoints.ContainsKey(key))
        {
            throw new InvalidOperationException();
        }

        foreach (var byContentUserPoint in byContentUserPoints[key])
        {
                yield return byContentUserPoint;
        }
    }

    public IEnumerable<int> ContestsBySubmissionType(SubmissionType submissionType)
    {
        List<int> list = new List<int>();

        if (!bySubmitionType.ContainsKey(submissionType))
        {
            return Enumerable.Empty<int>();
        }

        foreach (var submission in bySubmitionType[submissionType])
        {
            list.Add(submission);
        }
        return list;
    }
}
