namespace TP2_BDD_API_VOTING
{
    public class Candidats{
        public string nom_candidat { get; set; }
        public int vote { get; set; }
        public Candidats(string nom_candidat, int vote){
            this.nom_candidat = nom_candidat;
            this.vote = vote;
        }
        public Candidats(string nom_candidat){
            this.nom_candidat = nom_candidat;
            this.vote = 0;
        }
    }
}