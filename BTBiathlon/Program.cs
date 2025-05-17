using BTBiathlon;
using Interface_communication;
using Interface_communication.Utils;
using Interface_communication.Utils.Logging;

//Config
Config.NombrePhaseTour= 17;//Potentiellement faire une énum ?
Logger.NiveauLog = NiveauxLog.Info;

//IA
IntelligenceArtificielle ia = new IATest();
ia.Jouer();