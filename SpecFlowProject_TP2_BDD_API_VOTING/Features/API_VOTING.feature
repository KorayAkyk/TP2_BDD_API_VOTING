Feature: API_VOTING
@mytag

Scenario: Ajouter 4 candidats
	Given le candidat est 'Charlie Chaplin'
	And le candidat est 'Koray Akyurek'
	And le candidat est 'Charlotte Grime'
	And le candidat est 'Zinzin Ether'
	When compte des candidats
	Then il y a 4 candidats

Scenario: Candidat en double
	Given le candidat est 'Charlie Chaplin'
	And le candidat est 'Charlie Chaplin'
	When compte des candidats
	Then il y a 1 candidats
	
Scenario: Ajouter un vote pour un candidat invalide
	Given 2 votes pour 'Charlie Chaplin'
	When vote pour 'Charlie Chapin'
	Then erreur 'Le candidat que vous voulez ajouter est inconu'

Scenario: Ajouter un vote pour un candidat
	Given le candidat est 'Charlotte Grime'
	Given 5 votes pour 'Charlotte Grime'
	When vote pour 'Charlotte Grime'
	Then le candidat a 5 votes

Scenario: Verification si aucun gagnant sur le changement de tour
	Given le candidat est 'Charlie Chaplin'
	And le candidat est 'Koray Akyurek'
	And le candidat est 'Charlotte Grime'
	And le candidat est 'Zinzin Ether'

	Given 32 votes pour 'Charlie Chaplin'
	And 6 votes pour 'Koray Akyurek'
	And 1 votes pour 'Charlotte Grime'
	And 2 votes pour 'Zinzin Ether'
	When votes clos et creation d'un tour

	Given 19254 votes pour 'Charlie Chaplin'
	And 19254 votes pour 'Koray Akyurek'
	When votes clos
	Then il na pas de gagnant

Scenario: Compteur des votes blancs
	Given le tableau de candidats est
		 | Nom             |
		 | Charlie Chaplin |
		 | Koray Akyurek   |
		 | Charlotte Grime |
		 | Zinzin Ether    |
	Given 14 votes pour 'Charlie Chaplin'
	Given 12 votes pour 'Koray Akyurek'
	Given 35 votes pour 'Charlotte Grime'
	Given 66 votes pour 'Zinzin Ether'
	Given 215 votes blanc
	When compte des votes blancs
	Then il y a 215 votes au total
		
Scenario: Compteur de tout les votes
	Given le tableau de candidats est
		 | Nom             |
		 | Charlie Chaplin |
		 | Koray Akyurek   |
		 | Charlotte Grime |
		 | Zinzin Ether    |

	Given 0 votes pour 'Charlie Chaplin'
	Given 125 votes pour 'Koray Akyurek'
	Given 2 votes pour 'Charlotte Grime'
	Given 3 votes pour 'Zinzin Ether'
	And 1 votes blanc
	When compte de tout les votes
	Then il y a 131 votes au total
	
Scenario Outline: Ajouter des votes pour plusieurs candidats
	Given le candidat est '<candidats>'
	Given <vote_numero_1> votes pour '<candidats>'
	Given <vote_numero_2> votes pour '<candidats>'
	When vote pour '<candidats>'
	Then le candidat a <total> votes
	Examples: 
	  | candidats       | vote_numero_1 | vote_numero_2 | total |
	  | Charlie Chaplin | 9             | 2             | 11    |
	  | Koray Akyurek   | 12            | 1             | 13    |
    
Scenario: Verification si gagnant obtient la majorité au premier tour
	Given le candidat est 'Charlie Chaplin'
	And le candidat est 'Koray Akyurek'
	And le candidat est 'Charlotte Grime'
	And le candidat est 'Zinzin Ether'
	
	Given 9 votes pour 'Charlie Chaplin'
	And 4 votes pour 'Koray Akyurek'
	And 95 votes pour 'Charlotte Grime'
	And 11 votes pour 'Zinzin Ether'
	When votes clos
	Then le gagnant est 'Charlotte Grime'

Scenario: Verification candidat sur le changement d'un tour
	Given le candidat est 'Charlie Chaplin'
	And le candidat est 'Koray Akyurek'
	And le candidat est 'Charlotte Grime'
	And le candidat est 'Zinzin Ether'
	
	Given 2 votes pour 'Charlie Chaplin'
	And 12 votes pour 'Koray Akyurek'
	And 11 votes pour 'Charlotte Grime'
	And 1 votes pour 'Zinzin Ether'
	When votes clos et creation d'un tour
	And compte des candidats 
	Then il y a 2 candidats

Scenario: Verification si gagnant sur le changement de tour
	Given le candidat est 'Charlie Chaplin'
	And le candidat est 'Koray Akyurek'
	And le candidat est 'Charlotte Grime'
	And le candidat est 'Zinzin Ether'
	
	Given 12 votes pour 'Charlie Chaplin'
	And 63 votes pour 'Koray Akyurek'
	And 115 votes pour 'Charlotte Grime'
	And 151 votes pour 'Zinzin Ether'
	When votes clos et creation d'un tour
	
	Given 1455 votes pour 'Zinzin Ether'
	And 122 votes pour 'Charlotte Grime'
	When votes clos
	Then le gagnant est 'Zinzin Ether'


Scenario: Verification si gagnant au premier tour
	Given le candidat est 'Charlie Chaplin'
	And le candidat est 'Koray Akyurek'
	And le candidat est 'Charlotte Grime'
	And le candidat est 'Zinzin Ether'

	Given 7 votes pour 'Charlie Chaplin'
	And 4 votes pour 'Koray Akyurek'
	And 1 votes pour 'Charlotte Grime'
	And 2 votes pour 'Zinzin Ether'
	When votes clos
	Then le gagnant est 'Charlie Chaplin et Koray Akyurek'

Scenario: Verification si gagnant au deuxième tour
	Given changement de tour
	Given le candidat est 'Charlie Chaplin'
	And le candidat est 'Koray Akyurek'

	Given 89 votes pour 'Charlie Chaplin'
	And 99 votes pour 'Koray Akyurek'
	When votes clos
	Then le gagnant est 'Koray Akyurek'
	
Scenario: Ajouter plusieurs candidats
	Given le tableau de candidats est
	  | Nom             |
	  | Charlie Chaplin |
	  | Koray Akyurek   |
	  | Charlotte Grime |
	  | Zinzin Ether    |
	When compte des candidats
	Then il y a 4 candidats