using Interface_communication;
using Interface_communication.Utils.Logging;

namespace BTBiathlon.IAs;

public class IADefense : ModeleIA
{
    private int tourEnCours= 0;
    public override List<Message> PhaseTour(int tour, int phase, List<ReponseServeur> reponsesServeur)
    {
        TypePhaseEnum phaseEnum = base.GetPhase(tourEnCours);
        List<Message> messages = new List<Message>();
        Logger.Log(NiveauxLog.Info,tourEnCours.ToString());
        Logger.Log(NiveauxLog.Info,phaseEnum.ToString());

        if (phaseEnum == TypePhaseEnum.Nuit)
        {
            if (tourEnCours == 15)
            {
                messages.Add(new Message(Dictionnaire.Utiliser, ["DEFENSE"]));
            }
            messages.Add(new Message(Dictionnaire.Attaquer, ["0"]));
        }
        if (phaseEnum == TypePhaseEnum.Jour)
        {
            messages.Add(new Message(Dictionnaire.Piocher, ["0"]));
        }

        tourEnCours++;
        if (tourEnCours == 16)
        {
            tourEnCours = 0;
        }
        return messages;
    }
}