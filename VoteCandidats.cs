using System;
using System.Collections.Generic;

namespace TP2_BDD_API_VOTING
{
    public class VoteCandidats
    {
        public List<VoteTour> tour { get; set; }
        public int vote_tour_en_cours { get; set; }
        public Boolean fermeture { get; set; }

        public VoteCandidats()
        {
            tour = new List<VoteTour>();
            tour.Add(new VoteTour());
            vote_tour_en_cours = 1;
            fermeture = false;
        }

        public string prochain_tour()
        {
            string resultat = "";
            if (fermeture == false && vote_tour_en_cours > 1)
            {
                resultat = "Impossible d'avoir plus de deux tour";
            }       
            else
            {
                tour.Add(new VoteTour());
                vote_tour_en_cours++;
            }
            return resultat;
        }
    }
}