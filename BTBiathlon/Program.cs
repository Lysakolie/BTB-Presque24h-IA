using BTBiathlon;
using BTBiathlon.IAs;
using Interface_communication.Utils;
using Interface_communication.Utils.Logging;

//Config
Config.NombrePhaseTour= 1;//Potentiellement faire une énum ?
Logger.NiveauLog = NiveauxLog.Info;

//IA
ModeleIA morgane = new IAMorgane();
morgane.Jouer();