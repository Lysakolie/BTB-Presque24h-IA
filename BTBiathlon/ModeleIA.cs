using Interface_communication;

namespace BTBiathlon;

public abstract class ModeleIA : IntelligenceArtificielle
{
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
}