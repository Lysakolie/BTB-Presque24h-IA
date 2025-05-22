using BTBiathlon.Model;
using Interface_communication;
using Interface_communication.Utils.Logging;

namespace BTBiathlon.IAs;

public class IAMorgane : ModeleIA
{
    private int tourEnCours = 0;
    private int tourGeneral = 1;
    private int defense = 0;
    private int attaque = 0;
    private int savoir = 0;

    public override List<Message> PhaseTour(int tour, int phase, List<ReponseServeur> reponsesServeur)
    {
        List<Message> infos = GetMessagesDemandeInfo();
        InsertionDonnees(reponsesServeur);
        TypePhaseEnum phaseEnum = base.GetPhase(tourEnCours);
        List<Message> messages = new List<Message>();
        messages.AddRange(infos);
        
        if (tourGeneral == 1)
        {
            messages.AddRange(tour1(tourEnCours,phaseEnum));
        }
        else
        {
            if (phaseEnum == TypePhaseEnum.Jour)
            {
                Logger.Log(NiveauxLog.Info, $"Nombre de joueurs {Joueurs.Count}");
                if (Joueurs[Id].ScoreDefense > DegatsDameRouge)
                {
                    if (VerifMonstres(Monstres, Joueurs[Id].ScoreAttaque))
                    {
                        messages.Add(new Message(Dictionnaire.Piocher, ["2"]));
                        savoir += Pioches[2].Valeur;
                    }
                    else
                    {
                        messages.Add(new Message(Dictionnaire.Piocher, ["1"]));
                        attaque += Pioches[1].Valeur;
                    }
                }
                else
                {
                    messages.Add(new Message(Dictionnaire.Piocher, ["0"]));
                    defense += Pioches[0].Valeur;
                    
                }
            }
            else if (phaseEnum == TypePhaseEnum.Nuit)
            {
                int monstreAttaque = 0;

                for (int i = 0; i < Monstres.Count; i++)
                {
                    if (Monstres[i].Vie >= Monstres[monstreAttaque].Vie)
                    {
                        monstreAttaque = i;
                    }
                }


                if (tourEnCours == 15)
                {
                    messages.Add(new Message(Dictionnaire.Utiliser, ["DEFENSE"]));
                    messages.Add(new Message(Dictionnaire.Utiliser, ["ATTAQUE"]));
                    messages.Add(new Message(Dictionnaire.Utiliser, ["SAVOIR"]));
                }
                messages.Add(new Message(Dictionnaire.Attaquer,[$"{monstreAttaque}"]));
            }
        }

        tourEnCours++;
        if (tourEnCours == 16)
        {
            tourEnCours = 0;
            tourGeneral++;
        }

        return messages;
    }

    private bool VerifMonstres(List<Monstre> monstres, int attaque)
    {
        bool b = false;
        foreach (Monstre monstre in monstres)
        {
            if (attaque > monstre.Vie)
            {
                b = true;
            }
        }
        return b;
    }

    private List<Message> tour1(int tourEnCours, TypePhaseEnum phaseEnum)
    {
        List<Message> messages = new List<Message>();
        
        if (tourEnCours == 15)
        {
            messages.Add(new Message(Dictionnaire.Utiliser, ["ATTAQUE"]));
            messages.Add(new Message(Dictionnaire.Attaquer, ["0"]));
        }
        else
        {
            messages.Add(new Message(Dictionnaire.Piocher, ["1"]));
        }
        


        return messages;
    }
}