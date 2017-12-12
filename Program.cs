using System;
using System.Threading;
using System.Collections.Generic;

namespace do_an
{
    class Program
    {
        static void Main(string[] args)
        {
            string [,] screen = new string[20, 20];
            screen = resetScreen(screen);
            Element spaceship = new Element("=]>",screen.GetLength(0)/2,2);
            List<Element> meteorites = new List<Element>();
            screen[spaceship.l_x,spaceship.l_y] = spaceship.display;
            Random ran_dom = new Random();
            string meteorite = " (>";
            bool is_play = true;
            Console.Clear();
            int score = 0;

            void drawing(){
                while(true){
                    draw(screen,$"  Your score: {score}");
                    Thread.Sleep(10);
                }
            }

            void ranMeteorites(){
                while(is_play){
                    score++;
                    int temp = 100;
                    
                    for(int i = 0; i< meteorites.Count;i++){
                        meteorites[i].l_y--;
                        if(meteorites[i].l_x == spaceship.l_x && meteorites[i].l_y == spaceship.l_y){
                            is_play = false;
                            screen = resetScreen(screen);
                            screen[screen.GetLength(0)/2,screen.GetLength(1)/2-1] = "GAM";
                            screen[screen.GetLength(0)/2,screen.GetLength(1)/2] = "E O";
                            screen[screen.GetLength(0)/2,screen.GetLength(1)/2+1] = "VER";
                            return;
                        }
                        if(meteorites[i].l_y <= 0 ){
                            temp = i;
                        }
                        else{
                            screen[meteorites[i].l_x,meteorites[i].l_y] = meteorite;
                        }
                        screen[meteorites[i].l_x,meteorites[i].l_y+1] = "   ";
                    }
                    if(temp <= 0){
                        meteorites.RemoveAt(temp);
                    }
                    int ran_y = ran_dom.Next(1,screen.GetLength(0)-1);
                    meteorites.Add(new Element(meteorite,ran_y,screen.GetLength(1)-2));
                    screen[ran_y,screen.GetLength(1)-2] = meteorite;
                    Thread.Sleep(200);
                }
            }

            Thread ranMeteoritesThread = new Thread(ranMeteorites);
            Thread drawThread = new Thread(drawing);
            drawThread.Start();
            ranMeteoritesThread.Start();
            Console.CursorVisible = false;
            
            
            while(true){
                if(!is_play){
                    continue;
                }
                string key = Console.ReadKey().Key.ToString();
                switch(key){
                    case "UpArrow":
                        screen[spaceship.l_x,spaceship.l_y] = "   ";
                        spaceship.l_x--;
                        if(spaceship.l_x <= 0){
                            spaceship.l_x = 1;
                        }
                        screen[spaceship.l_x,spaceship.l_y] = spaceship.display;
                        break;
                    case "DownArrow":
                        screen[spaceship.l_x,spaceship.l_y] = "   ";
                        spaceship.l_x++;
                        if(spaceship.l_x >= screen.GetLength(0)-1){
                            spaceship.l_x = screen.GetLength(0)-2;
                        }
                        screen[spaceship.l_x,spaceship.l_y] = spaceship.display;
                        break;
                    case "RightArrow":
                        screen[spaceship.l_x,spaceship.l_y] = "   ";
                        spaceship.l_y++;
                        if(spaceship.l_y >= screen.GetLength(1)-1){
                            spaceship.l_y = screen.GetLength(1)-2;
                        }
                        screen[spaceship.l_x,spaceship.l_y] = spaceship.display;
                        break;
                    case "LeftArrow":
                        screen[spaceship.l_x,spaceship.l_y] = "   ";
                        spaceship.l_y--;
                        if(spaceship.l_y <= 0){
                            spaceship.l_y = 1;
                        }
                        screen[spaceship.l_x,spaceship.l_y] = spaceship.display;
                        break;
                    default:
                        break;
                }
            }
        }
        static void draw(string [,] screen, string str) {
            Console.SetCursorPosition(0,1);
            Console.WriteLine("  ---GAME SPACESHIP---");
            Console.WriteLine();
            for(int i = 0; i < screen.GetLength(0); i++) {
                for(int j = 0; j < screen.GetLength(1); j++) {
                    Console.Write(screen[i,j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine(str);
        }
        static string[,] resetScreen(string [,] screen) {
            for(int i = 0; i < screen.GetLength(0); i++) {
                for(int j = 0; j < screen.GetLength(1); j++) {
                    screen[i,j] = "   ";
                }
            }
            for(int i = 0 ;i<screen.GetLength(0); i++) {
                screen[i,0] = "  >";
                screen[i,screen.GetLength(1)-1] = " < ";
            }
            for(int i = 0 ;i<screen.GetLength(1);i++) {
                screen[0,i] = "---";
                screen[screen.GetLength(0)-1,i] = "---";
            }
            screen[0,0] = "  -";
            screen[screen.GetLength(0)-1,0] = "  -";
            screen[0,screen.GetLength(1)-1] = "-- ";
            screen[screen.GetLength(0)-1,screen.GetLength(1)-1] = "-- ";
            return screen;
        }
    }
}
