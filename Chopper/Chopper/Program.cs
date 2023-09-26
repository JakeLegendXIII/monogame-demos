

using Chopper.States.Splash;
using Chopper.Engine;

const int WIDTH = 1280;
const int HEIGHT = 720;

using var game = new MainGame(WIDTH, HEIGHT, new SplashState());
game.Run();
