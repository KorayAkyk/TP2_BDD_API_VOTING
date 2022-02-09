using System;
using System.Collections.Generic;

namespace TP2_BDD_API_VOTING
{
   public class VoteTour{
       public List<Candidats> candidats;
        public int nb_vote_total { get; set; }
        public int votes_blanc { get; set; }
        public bool fermeture_vote { get; set; }
        public int condition_victoire = 50;
        public VoteTour(){
            candidats = new List<Candidats>();
            nb_vote_total = 0;
            votes_blanc = 0;
            fermeture_vote = false;
        }
        public bool Verif_Si_Candidat_Existe(string nom){
            foreach (Candidats candidat in candidats){
                if (candidat.nom_candidat == nom){
                    return true;
                }
            }
            return false;
        }
        public Candidats Recuperation_Candidat(string nom){
            foreach (Candidats candidat in candidats){
                if (candidat.nom_candidat == nom)
                    return candidat;
            } return null;
        }
        public int Recuperation_Candidat_Index(string name){
            int i = 0;
            foreach (Candidats candidat in candidats){
                if (candidat.nom_candidat == name){
                    break;
                } i += 1;
            } return i;
        }
        public void Ajout_Candidat(string nom){
            if (!Verif_Si_Candidat_Existe(nom) && !fermeture_vote)
                candidats.Add(new Candidats(nom));
        }
        public void AddCandidates(List<string> noms){
            if(!fermeture_vote){
                foreach (string nom in noms){
                    Ajout_Candidat(nom);
                }
            }
        }
        public string Ajout_Votes_Candidats(string nom, int valeur){
            if (Verif_Si_Candidat_Existe(nom) && !fermeture_vote){
                candidats[Recuperation_Candidat_Index(nom)].vote += valeur;
                nb_vote_total += valeur;
                return null;
            }else{
                return "Le candidat que vous voulez ajouter est inconu";
            }
        }
        public string Ajout_Vote_Blanc(int val){
            votes_blanc += val;
            return "";
        }
        public string Recuperation_Candidat_Gagnant(bool fermetureVote, int nbTour){
            if (nb_vote_total == 0){
                return "Zéro vote";
            }
             if (nbTour == 1){
                foreach (Candidats candidat in candidats){
                    if (Candidats_Majorite(candidat))                  
                        return candidat.nom_candidat;            
                }
                Candidats _candidats = Candidats_Gagnant(candidats);
                List<Candidats> list = candidats;
                list.Remove(_candidats);
                Candidats _candidats1 = Candidats_Gagnant(list);
                return _candidats.nom_candidat + " et " + _candidats1.nom_candidat;
             }
             else if(nbTour == 2 && fermetureVote){
                 return Candidats_Gagnant(candidats).nom_candidat;
             }
             else{
                if (candidats[0].vote > candidats[1].vote){
                    return candidats[0].nom_candidat;
                }
                else if (candidats[0].vote < candidats[1].vote){
                    return candidats[1].nom_candidat;
                }
                else{
                    return "Zéro gagnant au second tour";
                }
             }
        }
        public bool Candidats_Majorite(Candidats _candidats){
            if (Pourcentage(_candidats) > condition_victoire){
                return true;
            } return false;
        }
        public Candidats Candidats_Gagnant(List<Candidats> list){
            Candidats gagnant = new Candidats("");
            foreach (Candidats _candidats in list){
                if (_candidats.vote >= gagnant.vote)
                    gagnant = _candidats;
            } return gagnant;
        }
        public float Pourcentage(Candidats _candidats){
            float a = (float)_candidats.vote / nb_vote_total * 100;
            return a;
        }
        public string Recuperation_Vote(){
            try{ 
               return (nb_vote_total + votes_blanc).ToString();
            }
            catch{
                return null;
            }
        }
        public string creationSecondTour(VoteCandidats vote){
            string res = vote.prochain_tour();
            if(vote.fermeture  && String.IsNullOrEmpty(res)){
                VoteTour tour1 = vote.tour[0];
                VoteTour fermeture_tour = vote.tour[1];
                VoteTour tour2 = vote.tour[2];
                
                Candidats candidat1 = tour1.Candidats_Gagnant(tour1.candidats);
                Candidats candidat2 = fermeture_tour.Candidats_Gagnant(fermeture_tour.candidats);

                candidat1.vote = 0;
                candidat2.vote = 0;
                tour1.candidats.Add(candidat1);
                tour2.candidats.Add(candidat2);
                return "";
            }
            else if(String.IsNullOrEmpty(res)){
                VoteTour tour1 = vote.tour[0];
                Candidats candidat1 = tour1.Candidats_Gagnant(candidats);

                List<Candidats> _candidats = candidats;
                _candidats.Remove(candidat1);

                Candidats candidat2 = tour1.Candidats_Gagnant(_candidats);
            
                VoteTour tour2 = vote.tour[1];
                candidat1.vote = 0;
                candidat2.vote = 0;
                tour2.candidats.Add(candidat1);
                tour2.candidats.Add(candidat2);

                return "";
            }else    
                return res;           
        }
    }
}