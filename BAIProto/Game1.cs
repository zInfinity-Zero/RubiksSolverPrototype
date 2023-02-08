using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
//using System.Drawing;
using System.Text;
using System.Threading;

namespace BAIProto
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Sprite[,] ff = new Sprite[3, 3];
        private Sprite[,] rf = new Sprite[3, 3];
        private Sprite[,] lf = new Sprite[3, 3];
        private Sprite[,] uf = new Sprite[3, 3];
        private Sprite[,] df = new Sprite[3, 3];
        private Sprite[,] bf = new Sprite[3, 3];
        private CubeM cube = new CubeM();
        private Texture2D tex;

        //ai code
        private Color targetfront, targettop, targetright;
        private string solvingstep, displaysolvingstep = "";
        private string sexymove = "4 5 1 2";
        private string reversesexymove = " 5 4 2 1";
        private TextManager solvetext = new TextManager();
        private SpriteFont a;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.PreferredBackBufferWidth = 1800;
            _graphics.ApplyChanges();
            cube.InitialiseCube();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            a = Content.Load<SpriteFont>("File");
            solvetext = new TextManager(a, solvingstep, new Vector2(700, 75), Color.Black);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            tex = Content.Load<Texture2D>("rubik");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    ff[i, j] = new Sprite(tex, new Vector2(500 + 100 * i, 300 + 100 * j), new Vector2(100, 100));
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    rf[i, j] = new Sprite(tex, new Vector2(800 + 100 * i, 300 + 100 * j), new Vector2(100, 100));
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    lf[i, j] = new Sprite(tex, new Vector2(200 + 100 * i, 300 + 100 * j), new Vector2(100, 100));
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    df[i, j] = new Sprite(tex, new Vector2(500 + 100 * i, 600 + 100 * j), new Vector2(100, 100));
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    uf[i, j] = new Sprite(tex, new Vector2(500 + 100 * i, 100 * j), new Vector2(100, 100));
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    bf[i, j] = new Sprite(tex, new Vector2(1300 + 100 * i, 100 + 100 * j), new Vector2(100, 100));
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            cube.UpdateCube(gameTime);
            solvetext.textContent = displaysolvingstep;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Color temp = cube.cubeorientationfront[i, j];
                    ff[i, j].spriteColor = temp;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Color temp = cube.cubeorientationup[i, j];
                    uf[i, j].spriteColor = temp;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Color temp = cube.cubeorientationdown[i, j];
                    df[i, j].spriteColor = temp;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Color temp = cube.cubeorientationright[i, j];
                    rf[i, j].spriteColor = temp;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Color temp = cube.cubeorientationleft[i, j];
                    lf[i, j].spriteColor = temp;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Color temp = cube.cubeorientationback[i, j];
                    bf[i, j].spriteColor = temp;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                cube.Turn(1);
                Thread.Sleep(100);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                cube.Turn(2);
                Thread.Sleep(100);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                cube.Turn(3);
                Thread.Sleep(100);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                cube.Turn(4);
                Thread.Sleep(100);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D5))
            {
                cube.Turn(5);
                Thread.Sleep(100);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D6))
            {
                cube.YTurn();
                Thread.Sleep(100);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                Thread.Sleep(1000);
                //for (int i = 0; i < 4; i++) //solving the cross
                //{
                    cube.UpdateCube(gameTime);
                    targetfront = cube.solvedcube[0][1, 0];
                    targettop = cube.solvedcube[2][1, 2];
                    //solvingstep += targetfront;
                    //piece on top  file cases
                    if (cube.wholecube[2][1, 2] == targettop && cube.wholecube[0][1, 0] == targetfront)
            
                        solvingstep  += " ";
                    else if (cube.wholecube[0][1, 0] == targettop && cube.wholecube[2][1, 2] == targetfront)
                        solvingstep += " 6 2 4 5";  // dont need brackets as only one line, looks more tidy 
                    else if (cube.wholecube[4][1, 0] == targettop && cube.wholecube[2][2, 1] == targetfront)
                        solvingstep += " 1 3";
                    else if (cube.wholecube[1][1, 0] == targettop && cube.wholecube[2][1, 0] == targetfront)
                        solvingstep += " 5 1 2 3";
                    else if (cube.wholecube[5][1, 0] == targettop && cube.wholecube[2][0, 1] == targetfront)
                        solvingstep += " 7 6";

                    else if (cube.wholecube[2][2, 1] == targettop && cube.wholecube[4][1, 0] == targetfront)
                        solvingstep += " 5 3 2 6";/////////////////////
                    else if (cube.wholecube[2][1, 0] == targettop && cube.wholecube[1][1, 0] == targetfront)
                        solvingstep += " 5 5 3 2 2";
                    else if (cube.wholecube[2][0, 1] == targettop && cube.wholecube[5][1, 0] == targetfront)
                        solvingstep += " 2 3 5 6";

                    // pieces on middle file
                    else if (cube.wholecube[4][0, 1] == targettop && cube.wholecube[0][2, 1] == targetfront)
                        solvingstep += " 3";
                    else if (cube.wholecube[5][2, 1] == targettop && cube.wholecube[0][0, 1] == targetfront)
                        solvingstep += " 6";
                    else if (cube.wholecube[0][2, 1] == targettop && cube.wholecube[4][0, 1] == targetfront)
                        solvingstep += " 2 4 5";////
                    else if (cube.wholecube[1][0, 1] == targettop && cube.wholecube[4][2, 1] == targetfront)
                        solvingstep += " 2 1 5";
                    else if (cube.wholecube[4][2, 1] == targettop && cube.wholecube[1][0, 1] == targetfront)
                        solvingstep += " 1 1 3 1 1";
                    else if (cube.wholecube[5][0, 1] == targettop && cube.wholecube[1][2, 1] == targetfront)
                        solvingstep += " 1 1 6 1 1";
                    else if (cube.wholecube[1][2, 1] == targettop && cube.wholecube[5][0, 1] == targetfront)
                        solvingstep += " 5 7 2";////
                    else if (cube.wholecube[0][0, 1] == targettop && cube.wholecube[5][2, 1] == targetfront)
                        solvingstep += " 5 8 2";

                    // pieces on 3rd file
                    else if (cube.wholecube[3][1, 0] == targettop && cube.wholecube[0][1, 2] == targetfront)
                        solvingstep += " 6 6";
                    else if (cube.wholecube[0][1, 2] == targettop && cube.wholecube[3][1, 0] == targetfront)
                        solvingstep += " 3 2 4 5";
                    else if (cube.wholecube[3][2, 1] == targettop && cube.wholecube[4][1, 2] == targetfront)
                        solvingstep += " a 6 6";// a = 50
                    else if (cube.wholecube[4][1, 2] == targettop && cube.wholecube[3][2, 1] == targetfront)
                        solvingstep += " 4 3 1";
                    else if (cube.wholecube[3][1, 2] == targettop && cube.wholecube[1][1, 2] == targetfront)
                        solvingstep += " a a 6 6";
                    else if (cube.wholecube[1][1, 2] == targettop && cube.wholecube[3][1, 2] == targetfront)
                        solvingstep += " a 4 3 1";
                    else if (cube.wholecube[3][0, 1] == targettop && cube.wholecube[5][2, 1] == targetfront)
                        solvingstep += " b 6 6";// b = 20
                    else if (cube.wholecube[5][2, 1] == targettop && cube.wholecube[3][0, 1] == targetfront)
                        solvingstep += " 8 6 7";

                //solved
                //if (cube.wholecube[2][1, 2] == targettop && cube.wholecube[0][1, 0] == targetfront)
                //{
                //    if (i != 3)
                //        cube.XTurn();
                //    else
                //    {
                //        //   
                //    }
                //}

                //cube.XTurn();


                solvingstep += " x";



                    
                //}
                string[] moves = solvingstep.Split(); // do at the end
                foreach (string move in moves)
                {
                    if (move == "1")
                        cube.Turn(1);
                    else if (move == "2")
                        cube.Turn(2);
                    else if (move == "3")
                        cube.Turn(3);
                    else if (move == "4")
                        cube.Turn(4);
                    else if (move == "5")
                        cube.Turn(5);
                    else if (move == "6")
                        cube.Turn(6);
                    else if (move == "7")
                        cube.Turn(7);
                    else if (move == "8")
                        cube.Turn(8);
                    else if (move == "9")
                        cube.Turn(9);
                    else if (move == "0")
                        cube.Turn(0);
                    else if (move == "a")
                        cube.Turn(50);
                    else if (move == "b")
                        cube.Turn(20);
                    else if (move == "x")
                        cube.XTurn();
                }
                displaysolvingstep += solvingstep;
                solvingstep = "";
                
            }

            ///
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Thread.Sleep(1000);
                cube.YTurn();
                cube.YTurn();
                cube.UpdateCube(gameTime);
                targettop = cube.solvedcube[2][0, 2];
                targetfront = cube.solvedcube[0][2, 0];
                targetright = cube.solvedcube[4][0, 0];
                //8 places the piece can be with 3 orientations
                //front bottom right
                if (cube.wholecube[4][0, 2] == targettop && cube.wholecube[3][2, 0] == targetfront && cube.wholecube[0][2, 2] == targetright)
                    solvingstep += sexymove * 2;
                else if (cube.wholecube[4][0, 2] == targetfront && cube.wholecube[3][2, 0] == targetright && cube.wholecube[0][2, 2] == targettop)
                    solvingstep += reversesexymove * 2;
                else if (cube.wholecube[4][0, 2] == targetfront && cube.wholecube[3][2, 0] == targetright && cube.wholecube[0][2, 2] == targettop)
                    solvingstep += sexymove;



            }


                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            foreach (Sprite s in ff)
            {
                s.DrawSprite(_spriteBatch, s.spriteTexture);
            }
            foreach (Sprite s in rf)
            {
                s.DrawSprite(_spriteBatch, s.spriteTexture);
            }
            foreach (Sprite s in lf)
            {
                s.DrawSprite(_spriteBatch, s.spriteTexture);
            }
            foreach (Sprite s in uf)
            {
                s.DrawSprite(_spriteBatch, s.spriteTexture);
            }
            foreach (Sprite s in df)
            {
                s.DrawSprite(_spriteBatch, s.spriteTexture);
            }
            foreach (Sprite s in bf)
            {
                s.DrawSprite(_spriteBatch, s.spriteTexture);
            }
            // TODO: Add your drawing code here
            solvetext.DrawText(_spriteBatch);
            base.Draw(gameTime);
        }
    }
}