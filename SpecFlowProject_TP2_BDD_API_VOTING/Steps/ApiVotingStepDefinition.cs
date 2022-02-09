using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using TechTalk.SpecFlow;
using TP2_BDD_API_VOTING;

namespace SpecFlowProject_TP2_BDD_API_VOTING.Steps
{
    [Binding]
    public sealed class ApiVotingStepDefinition
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;
        private VoteCandidats vote;
        private string Resultat_Vote = "";

        public ApiVotingStepDefinition(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            vote = new VoteCandidats();
        }

        [Given("le candidat est '(.*)'")]
        public void ajouter_candidat(string nom)
        {
            VoteTour tour = vote.tour[vote.vote_tour_en_cours - 1];
            tour.Ajout_Candidat(nom);

        }

        [Given(@"le tableau de candidats est")]
        public void ajouter_tab_candidat(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                VoteTour tour = vote.tour[vote.vote_tour_en_cours - 1];
                tour.Ajout_Candidat(row["Nom"]);
            }
        }

        [Given("(.*) votes pour '(.*)'")]
        public void ajouterVotesPour(int votesNumber, string name)
        {
            VoteTour tour = vote.tour[vote.vote_tour_en_cours - 1];
            Resultat_Vote = tour.Ajout_Votes_Candidats(name, votesNumber);
        }

        [Given("(.*) votes blanc")]
        public void ajouterVotesBlanc(int votesBlancs)
        {
            VoteTour tour = vote.tour[vote.vote_tour_en_cours - 1];
            Resultat_Vote = tour.Ajout_Vote_Blanc(votesBlancs);
        }

        [Given("changement de tour")]
        public void changementTour()
        {
            Resultat_Vote = vote.prochain_tour();
        }

        [When("vote pour '(.*)'")]
        public void ajouterVotePour(string nom)
        {
            if (String.IsNullOrEmpty(Resultat_Vote))
            {
                VoteTour tour = vote.tour[vote.vote_tour_en_cours - 1];
                Resultat_Vote = tour.Recuperation_Candidat(nom).vote.ToString();
            }

        }

        [When("compte de tout les votes")]
        public void compteurVotes()
        {
            VoteTour tour = vote.tour[vote.vote_tour_en_cours - 1];
            Resultat_Vote = tour.Recuperation_Vote();
        }

        [When("compte des candidats")]
        public void compteurCandidats()
        {
            VoteTour tour = vote.tour[vote.vote_tour_en_cours - 1];
            Resultat_Vote = tour.candidats.Count.ToString();
        }

        [When("compte des votes blancs")]
        public void compteurVoteBlancs()
        {
            VoteTour tour = vote.tour[vote.vote_tour_en_cours - 1];
            Resultat_Vote = tour.votes_blanc.ToString();
        }

        [When("compte du pourcentage pour '(.*)'")]
        public void compteurPourcentagePour(string nom)
        {
            if (String.IsNullOrEmpty(Resultat_Vote))
            {
                VoteTour tour = vote.tour[vote.vote_tour_en_cours - 1];
                Resultat_Vote = tour.Pourcentage(tour.Recuperation_Candidat(nom)).ToString();
            }

        }

        [When("votes clos")]
        public void votesClos()
        {
            VoteTour tour = vote.tour[vote.vote_tour_en_cours - 1];
            Resultat_Vote = tour.Recuperation_Candidat_Gagnant(vote.fermeture, vote.vote_tour_en_cours);
        }

        [When("votes clos et creation d'un tour")]
        public void votesClosEtCreationTour()
        {
            VoteTour tour = vote.tour[vote.vote_tour_en_cours - 1];
            Resultat_Vote = tour.creationSecondTour(vote);
        }

        

        [Then("le candidat a (.*) votes")]
        public void recupereCandidatVotes(string result)
        {
            Resultat_Vote.Should().Be(result);

        }

        [Then("il y a (.*) candidats")]
        public void recupereNombreCandidats(string result)
        {
            Resultat_Vote.Should().Be(result);

        }

        [Then("il y a (.*) votes au total")]
        public void recupereVotesTotal(string result)
        {
            Resultat_Vote.Should().Be(result);
        }

        [Then("le gagnant est '(.*)'")]
        public void leGagnantEst(string result)
        {
            Resultat_Vote.Should().Be(result);
        }

        [Then("impossible de cree un nouveau tour")]
        public void creationImpossibleTour()
        {
            Resultat_Vote.Should().Be("Impossible d'avoir plus de 2 tours");
        }

        [Then("il na pas de gagnant")]
        public void aucunGagnant()
        {
            Resultat_Vote.Should().Be("Zéro gagnant au second tour");
        }

        [Then("erreur '(.*)'")]
        public void erreurMessage(string result)
        {
            Resultat_Vote.Should().Be(result);
        }
    }
}
    
