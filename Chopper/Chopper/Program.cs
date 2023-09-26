

using Chopper.States;

const int WIDTH = 1280;
const int HEIGHT = 720;

using var game = new Chopper.MainGame(WIDTH, HEIGHT, new SplashState());
game.Run();
