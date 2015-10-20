using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace BarFightEngineM2
{
    public struct Settings
    {
        public bool AA;
        public int width;
        public int height;
        public bool rumble;
        public bool sfx;
        public bool music;
        public bool fullscreen;
    }
    public class Options
    {
        private int timer;
        private const int inputDelay = 15;
        private static int[][] screenResos = { new int[] { 1280, 720 },
        new int[] {1920,1080},
            new int[] { 3840, 2160 } };
        private int resoIndex = 0;
        private Menu menu;
        private string message;
        private BarFight parent;
        private int width, height;
        private int x, y;
        private Settings settings;
        private SpriteFont font;

        public Options(ContentManager content,BarFight parent)
        {
            this.parent = parent;
            Texture2D AA,resolution,rumble,sfx,music,fullscreen,accept,exit;
            AA = content.Load<Texture2D>("antialiasing");
            resolution = content.Load<Texture2D>("resolution");
            rumble = content.Load<Texture2D>("rumble");
            sfx = content.Load<Texture2D>("soundfx");
            music = content.Load<Texture2D>("music");
            fullscreen = content.Load<Texture2D>("fullscreen");
            accept = content.Load<Texture2D>("start");
            exit = content.Load<Texture2D>("exit");
            x = parent.ScreenWidth / 2 - (AA.Width / 2);
            y = parent.ScreenHeight / 2 - ((4 + AA.Height * 5) / 2);
            width = AA.Width;
            height = AA.Height;
            menu = new Menu(4, new Rectangle(x, y, width, height), AA, resolution, rumble, sfx, music,fullscreen,accept,exit);
            settings = new Settings();
            settings.AA = true;
            settings.height = screenResos[0][1];
            settings.width = screenResos[0][0];
            settings.music = true;
            settings.rumble = true;
            settings.sfx = true;
            font = content.Load<SpriteFont>("Font");
            message = "";

        }

        public void resetTimer()
        {
            timer = inputDelay;
        }

        public void update(GamePadState state)
        {
            menu.update(state);
            if(timer < 0)
            {
                if(state.ThumbSticks.Left.X >0.2)
                {
                    timer = inputDelay;
                    switch(menu.SelectedIndex)
                    {
                        case 0:
                            {
                                settings.AA = true;
                                break;
                            }
                        case 1:
                            {
                                if (resoIndex >= screenResos.Length-1)
                                {
                                    resoIndex = 0;
                                }
                                else
                                    resoIndex++;
                                break;
                            }
                        case 2:
                            {
                                settings.rumble = true;
                                break;
                            }
                        case 3:
                            {
                                settings.sfx = true;
                                break;
                            }
                        case 4:
                            {
                                settings.music = true;
                                break;
                            }
                        case 5:
                            {
                                settings.fullscreen = true;
                                break;
                            }

                    }
                }
                else
                if(state.ThumbSticks.Left.X < -0.2)
                {
                    timer = inputDelay;
                    switch(menu.SelectedIndex)
                    {
                        case 0:
                            {
                                settings.AA = false;
                                break;
                            }
                        case 1:
                            {
                                if(resoIndex == 0)
                                {
                                    resoIndex = screenResos.Length - 1;
                                }
                                else
                                {
                                    resoIndex--;
                                }
                                break;
                            }
                        case 2:
                            {
                                settings.rumble = false;
                                break;
                            }
                        case 3:
                            {
                                settings.sfx = false;
                                break;
                            }
                        case 4:
                            {
                                settings.music = false;
                                break;
                            }
                        case 5:
                            {
                                settings.fullscreen = false;
                                break;
                            }

                    }
                }

                if(state.Buttons.X == ButtonState.Pressed)
                {
                    timer = inputDelay;
                    switch(menu.SelectedIndex)
                    {
                        case 6:
                            {
                                settings.width = screenResos[resoIndex][0];
                                settings.height = screenResos[resoIndex][1];
                                parent.applySettings(settings);
                                parent.resetTimers();
                                parent.gameState = BarFight.GameState.MainMenu;
                                break;
                            }
                        case 7:
                            {
                                parent.resetTimers();
                                parent.gameState = BarFight.GameState.MainMenu;
                                break;
                            }
                    }
                }
            }
            else
            {
                timer--;
            }
            switch(menu.SelectedIndex)
            {
                case 0:
                    {
                        if(settings.AA)
                        {
                            message = "true";
                        }
                        else
                        {
                            message = "false";
                        }
                        break;
                    }
                case 1:
                    {
                        message = "" + screenResos[resoIndex][0] + " * " + screenResos[resoIndex][1];
                        break; 
                    }
                case 2:
                    {
                        if(settings.rumble)
                        {
                            message = "true";
                        }
                        else
                        {
                            message = "false";
                        }
                        break;
                    }
                case 3:
                    {
                        if(settings.sfx)
                        {
                            message = "true";
                        }
                        else
                        {
                            message = "false";
                        }
                        break;
                    }
                case 4:
                    {
                        if (settings.music)
                        {
                            message = "true";
                        }
                        else
                        {
                            message = "false";
                        }
                        break;
                    }
                case 5:
                    {
                        if(settings.fullscreen)
                        {
                            message = "true";
                        }
                        else
                        {
                            message = "false";
                        }
                        break;
                    }
                default:
                    {
                        message = "";
                        break;
                    }
            }
        }

        public void draw(SpriteBatch sprbatch)
        {
            menu.draw(sprbatch);
            //draw the text next to the menu
            sprbatch.DrawString(font, message, new Vector2(x + width, y + (height* menu.SelectedIndex)), Color.IndianRed);
        }

    }
}
