using Interface_communication;

namespace BTBiathlon.IAs;

public class IALogique : IntelligenceArtificielle
{
    public override List<Message> GetProtocoleDemarragePartie()
    {
        return [new Message("Bêta-Testeurs-Drunken")];
    }

    public override List<Message> PhaseTour(int tour, int phase, List<ReponseServeur> reponsesServeur)
    {
        List<Message> messages = new List<Message>();
        if (phase != 16) //Pas nuit de sang
        {
            if (phase % 4 == 0) //NUIT 
            {
                messages.Add(new Message(Dictionnaire.Utiliser, ["SAVOIR"]));
                messages.Add(new Message(Dictionnaire.Attaquer, ["0"]));
            }
            else //Jour
            {
                messages.Add(new Message(Dictionnaire.Piocher, ["2"]));
            }
        }
        return messages;
    }
}