Feature: Scrutin

Scenario: Voting not closed
	Given candidat 1 has 1 votes
	And candidat 2 has 2 votes
	When get winner
	Then exception 'Sondage non cloturé' is thrown

Scenario: Voting 1 round
	Given candidat 1 has 1 votes
	Given candidat 2 has 2 votes
	When the voting is closed
	Then winner is candidat 2

Scenario: Get votes
	Given candidat 1 has 0 votes
	And candidat 2 has 2 votes
	Then candidat 1 has 0 votes and 0%
	And candidat 2 has 2 votes and 100%

Scenario: Voting 2 rounds
	Given candidat 1 has 2 votes
	And candidat 2 has 0 votes
	And candidat 3 has 2 votes
	When the voting is closed
	Then candidat 1 is in vote
	And candidat 1 has 0 votes
	And candidat 2 is not in vote
	And candidat 3 is in vote
	And candidat 3 has 0 votes

Scenario: Voting 2 rounds with winner
	Given it is second round
	Given candidat 1 has 2 votes
	And candidat 2 has 0 votes
	When the voting is closed
	Then winner is candidat 1

Scenario: Voting 2 round with egality
	Given it is second round
	And candidat 1 has 2 votes
	And candidat 2 has 0 votes
	When the voting is closed
	Then it is an egality

	
	