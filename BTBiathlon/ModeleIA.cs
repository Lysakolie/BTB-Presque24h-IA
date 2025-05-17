using BTBiathlon.Model;
using Interface_communication;
using Interface_communication.Utils.Logging;

namespace BTBiathlon;

public abstract class ModeleIA : IntelligenceArtificielle
{
    private int degatsDameRouge = 10;
    private List<Carte> modelePioches = new();

    /// <summary>
    /// Dégâts actuels de la dame en rouge
    /// </summary>
    protected int DegatsDameRouge => degatsDameRouge;

    /// <summary>
    /// Etat de la pioche
    /// </summary>
    protected List<Carte> Pioches => modelePioches;


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

    public override List<Message> GetProtocoleDemarragePartie()
    {
        return [new Message("Bêta-Testeurs")];
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
            else if (reponseServeur.MessageIa.VerbeMessage == Dictionnaire.Pioches)
            {
                InsertionPioches(reponseServeur);
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
    
    private void InsertionPioches(ReponseServeur reponse)
    {
        List<string[]> pioches = new();
        List<Carte> modelePioches = new();

        for (int i = 0; i < reponse.Arguments.Length; i += 2)
        {
            pioches.Add([reponse.Arguments[i], reponse.Arguments[i + 1]]);
        }
        
        foreach (string[] carte in pioches)
        {
            TypeCarteEnum type = GetTypeCarte(carte);
            modelePioches.Add(new Carte(int.Parse(carte[1]), type));
        }
        
        this.modelePioches = modelePioches;
    }

    private static TypeCarteEnum GetTypeCarte(string[] carte)
    {
        TypeCarteEnum type;
        switch (carte[0])
        {
            case "DEFENSE":
                type = TypeCarteEnum.Defense;
                break;
            case "ATTAQUE":
                type = TypeCarteEnum.Attaque;
                break;
            case "SAVOIR":
                type = TypeCarteEnum.Savoir;
                break;
            default:
                Logger.Log(NiveauxLog.Erreur, $"Type de carte inconnu : {carte[0]}");
                throw new Exception($"Type de carte inconnu : {carte[0]}");
        }

        return type;
    }
}