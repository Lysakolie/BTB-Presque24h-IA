using BTBiathlon.Model;
using Interface_communication;

namespace BTBiathlon;

public abstract class ModeleIA : IntelligenceArtificielle
{
    
    protected List<Joueur> joueurs = new List<Joueur>();
    protected List<Monstre> monstres = new List<Monstre>();
    protected TypePhaseEnum GetPhase(int phase)
    {
        var phaseEnum = TypePhaseEnum.Jour; // Si c'est pas la nuit ou la nuit de sang, c'est le jour
        if (phase == 16) // 17ème phase -> nuit de sang
        {
            phaseEnum = TypePhaseEnum.NuitSang;
        }
        else if (phase % 4 == 0) // Une phase sur 4 est une phase de nuit
        {
            phaseEnum = TypePhaseEnum.Nuit;
        }
        return phaseEnum;
    }

    /// <summary>
    /// Effectue les demandes d'informations au serveur
    /// </summary>
    /// <returns>Liste des messages à envoyer</returns>
    protected List<Message> GetMessagesDemandeInfo()
    {
        return
        [
            new Message(Dictionnaire.Degats),
            new Message(Dictionnaire.Joueur),
            new Message(Dictionnaire.Monstres),
            new Message(Dictionnaire.Pioches)
        ];
    }

    /// <summary>
    /// Insère les données du serveur en mémoire
    /// </summary>
    /// <param name="reponsesServeur">Réponses du serveur à <see cref="GetMessagesDemandeInfo"/></param>
    protected void InsertionDonnees(List<ReponseServeur> reponsesServeur)
    {
        throw new NotImplementedException();
    }


    private void getInfoJoueur(ReponseServeur reponseServeur)
    {
        joueurs.Clear();

        string[] infosJoueurs = reponseServeur.Arguments;
        int nbValeursParJoueur = 4;

        for (int i = 0; i < infosJoueurs.Length; i += nbValeursParJoueur)
        {
            if (i + 3 >= infosJoueurs.Length)
                break;

            Joueur joueur = new Joueur
            {
                Id = i / nbValeursParJoueur,
                Vie = int.Parse(infosJoueurs[i]),
                ScoreDefense = int.Parse(infosJoueurs[i + 1]),
                ScoreAttaque = int.Parse(infosJoueurs[i + 2]),
                ScoreSavoir = int.Parse(infosJoueurs[i + 3])
            };

            joueurs.Add(joueur);
        }
    }

    private void getInfoMonstre(ReponseServeur reponseServeur)
    {
        monstres.Clear();
        
        string[] infosMonstres = reponseServeur.Arguments;
        
        int nbValeursParMonstre = 2;

        for (int i = 0; i < infosMonstres.Length; i += nbValeursParMonstre)
        {
            Monstre monstre = new Monstre()
            {
                Id = i / nbValeursParMonstre,
                Vie = int.Parse(infosMonstres[i]),
                Savoir = int.Parse(infosMonstres[i + 1]),
            };
            
            monstres.Add(monstre);
        }
    }

    
    
}