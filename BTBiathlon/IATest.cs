using Interface_communication;

namespace BTBiathlon;

public class IATest : ModeleIA
{
    public override List<Message> PhaseTour(int tour, int phase, List<ReponseServeur> reponsesServeur)
    {
        return GetMessagesDemandeInfo();
    }
}