using System.Collections;

namespace scrutin;

public class Scrutin
{
    public bool isSecondRound = false;
    private int winner;
    private bool isClosed = false;
    private bool isEgalite = false;
    private Dictionary<int, int> votes = new Dictionary<int, int>();
    public void setCandidat(int candidat, int nbVotes)
    {
        if (isClosed) throw new Exception("Sondage cloturé");
        votes.Add(candidat, nbVotes);
    }

    public void close()
    {
        isClosed = true;

        // Si c'est le premier tour, chercher le gagnant ou les deux candidats ayant le plus de votes
        if (!isSecondRound)
        {
            int nbVotesTotal = votes.Values.Sum();
            winner = votes.Keys.FirstOrDefault(x => (double)votes[x] / nbVotesTotal > 0.5);

            if (winner == 0)
            {
                var topTwoCandidates = votes.OrderByDescending(x => x.Value).Take(2).Select(x => x.Key).ToList();
                votes.Clear();
                votes.Add(topTwoCandidates[0], 0);
                votes.Add(topTwoCandidates[1], 0);
                isSecondRound = true;
            }
        }
        // Si c'est le deuxième tour, chercher le gagnant ou indiquer une égalité
        else
        {
            int nbVotesTotal = votes.Values.Sum();
            winner = votes.Keys.FirstOrDefault(x => (double)votes[x] / nbVotesTotal > 0.5);
            if (winner == 0) isEgalite = true;
        }
    }

    public int getPourcentage(int candidat)
    {
        int totalVotes = votes.Values.Sum();
        if (totalVotes == 0) return 0;
        return (votes[candidat] * 100) / totalVotes;
    }

    public int getVotes(int candidat)
    {
        return votes[candidat];
    }

    public bool isInVote(int candidat)
    {
        return votes.ContainsKey(candidat);
    }

    public void setSecondRound()
    {
        isSecondRound = true;
    }

    public int getWinner()
    {
        if (!isClosed) throw new Exception("Sondage non cloturé");
        if (isEgalite) throw new Exception("Egality");
        return winner;

    }
}