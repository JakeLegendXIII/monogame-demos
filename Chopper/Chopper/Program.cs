using Chopper.States.Splash;
using Chopper.Engine;
using Chopper.States.Dev;
using System;
using Engine2DPipelineExtensions;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using Newtonsoft.Json;

const int WIDTH = 1280;
const int HEIGHT = 720;

AnimationData data = new AnimationData();
AnimationFrameData frame1 = new AnimationFrameData();
AnimationFrameData frame2 = new AnimationFrameData();

frame1.X = 1;
frame1.Y = 1;
frame1.CellHeight = 1;
frame1.CellWidth = 1;

frame2.X = 2;
frame2.Y = 2;
frame2.CellHeight = 2;
frame2.CellWidth = 2;

data.AnimationSpeed = 1;
data.IsLooping = true;
data.Frames = new List<AnimationFrameData>();
data.Frames.Add(frame1);
data.Frames.Add(frame2);

string fileName = "test.json";
string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);
File.WriteAllText(fileName, jsonString);


//using (var game = new MainGame(WIDTH, HEIGHT, new SplashState(), true))
//{
//    game.IsFixedTimeStep = true;
//    game.TargetElapsedTime = TimeSpan.FromMilliseconds(1000.0f / 60);
//    game.Run();
//}
// used to run in non-game mode so you can test out things like particles
//using (var game = new MainGame(WIDTH, HEIGHT, new DevState(), true))
//{
//    game.IsFixedTimeStep = true;
//    game.TargetElapsedTime = TimeSpan.FromMilliseconds(1000.0f / 60);
//    game.Run();
//}


