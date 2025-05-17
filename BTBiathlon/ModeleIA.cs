using BTBiathlon.Model;
using Interface_communication;
using Interface_communication.Utils.Logging;

namespace BTBiathlon;

public abstract class ModeleIA : IntelligenceArtificielle
{
    private int degatsDameRouge = 10;
    private List<Carte> modelePioches = new();
    private int id = -1;

    /// <summary>
    /// Dégâts actuels de la dame en rouge
    /// </summary>
    protected int DegatsDameRouge => degatsDameRouge;

    /// <summary>
    /// Etat de la pioche
    /// </summary>
    protected List<Carte> Pioches => modelePioches;


    private List<Joueur> joueurs = new List<Joueur>();
    private List<Monstre> monstres = new List<Monstre>();
    
    
    protected List<Joueur> Joueurs => joueurs;
    protected List<Monstre> Monstres => monstres;

    protected int Id => id;

    protected TypePhaseEnum GetPhase(int phase)
    {
        int phaseServeur = phase+1;
        var phaseEnum = TypePhaseEnum.Jour; // Si c'est pas la nuit ou la nuit de sang, c'est le jour
        if (phaseServeur == 17) // 17ème phase -> nuit de sang
        {
            phaseEnum = TypePhaseEnum.NuitSang;
        }
        else if (phaseServeur % 4 == 0) // Une phase sur 4 est une phase de nuit
        {
            phaseEnum = TypePhaseEnum.Nuit;
        }
        return phaseEnum;
    }

    public override List<Message> GetProtocoleDemarragePartie()
    {
        return [new Message("Les Bêta Testeurs du BUT")];
    }


    /// <summary>
    /// Effectue les demandes d'informations au serveur
    /// </summary>
    /// <returns>Liste des messages à envoyer</returns>
    protected List<Message> GetMessagesDemandeInfo()
    {
        return
        [
            new Message(Dictionnaire.Degats, reponseContientVerbe: false),
            new Message(Dictionnaire.Joueur, reponseContientVerbe: false),
            new Message(Dictionnaire.Monstres, reponseContientVerbe: false),
            new Message(Dictionnaire.Pioches, reponseContientVerbe: false),
            new Message(Dictionnaire.Moi, reponseContientVerbe: false)
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
            
            else if (reponseServeur.MessageIa.VerbeMessage == Dictionnaire.Joueur)
            {
                InsertionJoueurs(reponseServeur);
            }
            else if (reponseServeur.MessageIa.VerbeMessage == Dictionnaire.Monstres)
            {
                InsertionMonstres(reponseServeur);
            }
            else if (reponseServeur.MessageIa.VerbeMessage == Dictionnaire.Moi)
            {
                Joueur moi = LisJoueur(0, 0, reponseServeur.Arguments);
                Joueur moiDansLaListe = this.Joueurs.Find(x => moi.Equals(x));
                this.id = moiDansLaListe.Id;
            }
        }
    }


    private void InsertionJoueurs(ReponseServeur reponseServeur)
    {
        joueurs.Clear();

        string[] infosJoueurs = reponseServeur.Arguments;
        int nbValeursParJoueur = 4;

        for (int i = 0; i < infosJoueurs.Length; i += nbValeursParJoueur)
        {
            if (i + 3 >= infosJoueurs.Length)
                break;

            Joueur joueur = LisJoueur(i, i / nbValeursParJoueur, infosJoueurs);

            joueurs.Add(joueur);
        }
    }

    private static Joueur LisJoueur(int i, int id, string[] infosJoueurs)
    {
        Joueur joueur = new Joueur
        {
            Id = id,
            Vie = int.Parse(infosJoueurs[i]),
            ScoreDefense = int.Parse(infosJoueurs[i + 1]),
            ScoreAttaque = int.Parse(infosJoueurs[i + 2]),
            ScoreSavoir = int.Parse(infosJoueurs[i + 3])
        };
        return joueur;
    }

    private void InsertionMonstres(ReponseServeur reponseServeur)
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