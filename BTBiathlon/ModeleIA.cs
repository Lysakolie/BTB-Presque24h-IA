using BTBiathlon.Model;
using Interface_communication;

namespace BTBiathlon;

public abstract class ModeleIA : IntelligenceArtificielle
{
    private int degatsDameRouge = 10;

    /// <summary>
    /// Dégâts actuels de la dame en rouge
    /// </summary>
    protected int DegatsDameRouge => degatsDameRouge;

    
    
    protected List<Joueur> joueurs = new List<Joueur>();
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
        foreach (ReponseServeur reponseServeur in reponsesServeur)
        {
            if (reponseServeur.MessageIa.VerbeMessage == Dictionnaire.Degats)
            {
                InsertionDegatsDameRouge(reponseServeur);
            }
        }
    }


    private void getInfoJoueur(ReponseServeur reponseServeur)
    {
        
    }

    private void InsertionDegatsDameRouge(ReponseServeur reponse)
    {
        this.degatsDameRouge = int.Parse(reponse.Arguments[0]);
    } 
}