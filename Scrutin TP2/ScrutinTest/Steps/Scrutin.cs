namespace ScrutinTest;
using Xunit;
[Binding]
public class Scrutin
{
    private scrutin.Scrutin _scrutin = null!;
    private Exception _exception;
    
    [BeforeScenario]
    public void SetupTest()
    {
        _exception = null;
        _scrutin = new scrutin.Scrutin();
    }
    
    [Given(@"candidat (.*) has (.*) votes")]
    public void GivenCandidatHasVotes(int candidat, int nbVotes)
    {
        _scrutin.setCandidat(candidat, nbVotes);
    }

    [When(@"the voting is closed")]
    public void WhenTheVotingIsClosed()
    {
        _scrutin.close();
    }

    [Then(@"exception '(.*)' is thrown")]
    public void ThenExceptionIsThrown(string expectedErrorMessage)
    {
        Assert.NotNull(_exception);
        Assert.Equal(expectedErrorMessage, _exception.Message);
    }

    [Then(@"winner is candidat (.*)")]
    public void ThenWinnerIsCandidat(int candidat)
    {
        int winner = _scrutin.getWinner();
        Assert.Equal(candidat, winner);
    }

    [Then(@"candidat (.*) has (.*) votes and (.*)%")]
    public void ThenCandidatHasVotesAnd(int candidat, int votes, int pourcentage)
    {
        Assert.Equal(_scrutin.getVotes(candidat), votes);
        Assert.Equal(_scrutin.getPourcentage(candidat), pourcentage);
    }

    [Then(@"candidat (.*) is in vote")]
    public void ThenCandidatIsInVote(int candidat)
    {
        Assert.True(_scrutin.isInVote(candidat));
    }

    [Then(@"candidat (.*) has (.*) votes")]
    public void ThenCandidatHasVotes(int candidat, int votes)
    {
        Assert.Equal(_scrutin.getVotes(candidat), votes);
    }

    [Then(@"candidat (.*) is not in vote")]
    public void ThenCandidatIsNotInVote(int candidat)
    {
        Assert.False(_scrutin.isInVote(candidat));
    }

    [Given(@"it is second round")]
    public void GivenItIsSecondRound()
    {
        _scrutin.setSecondRound();
    }

    [Then(@"it is an egality")]
    public void ThenItIsAnEgality()
    {
        try
        {
            _scrutin.getWinner();
        }
        catch (Exception e)
        {
            Assert.NotNull(e);
        }
    }

    [When(@"get winner")]
    public void WhenGetWinner()
    {
        try
        {
            int winner = _scrutin.getWinner();
        }
        catch (Exception e)
        {
            _exception = e;
        }
    }
}