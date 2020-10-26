//-------------------------------------------------------------------------------------
//	ComponentPatternExample.cs
//-------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ComponentPatternExample
{

    public class ComponentPatternExample : MonoBehaviour
    {
        RPGGame rpgGame = new RPGGame();

        void Start()
        {
            rpgGame.Start();
        }

        void Update()
        {
            rpgGame.Update();
        }
    }


    class RPGGame
    {
        public int velocity;
        public int x = 0, y = 0;

       
        public WorldX worldX = new WorldX();
        public GraphicsX graphicsX = new GraphicsX();

       
        private InputComponent inputComponent;
        private PhysicsComponent physicsComponent;
        private GraphicsComponent graphicsComponent;

        public List<BaseComponent> ComponentList = new List<BaseComponent>();
  
        int componentAmount = -1;

        public void Start()
        {
            inputComponent = new PlayerInputComponent();
            physicsComponent = new PlayerPhysicsComponent();
            graphicsComponent = new PlayerGraphicsComponent();

            ComponentList.Add(inputComponent);
            ComponentList.Add(physicsComponent);
            ComponentList.Add(graphicsComponent);

            Debug.Log("Game Components Initialization Finish...");
            Debug.Log("Please enter LeftArrow or RightArrow button to play...");
        }

        public void Update()
        {
            if (ComponentList == null)
            {
                return;
            }
            componentAmount = ComponentList.Count;
            for (int i = 0; i < componentAmount; i++)
            {
                ComponentList[i].Update(this);
            }
        }
    };


    class PlayerInputComponent : InputComponent
    {
        public void Update(RPGGame game)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                game.velocity -= WALK_ACCELERATION;
                Debug.Log(" game velocity= " + game.velocity.ToString());
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                game.velocity += WALK_ACCELERATION;
                Debug.Log(" game velocity= " + game.velocity.ToString());
            }
        }

        private int WALK_ACCELERATION = 1;
    }


    class PlayerPhysicsComponent : PhysicsComponent
    {
        public void Update(RPGGame game)
        {
            game.x += game.velocity;

            //Handle Physics...

        }
    }


    class PlayerGraphicsComponent : GraphicsComponent
    {
        public void Update(RPGGame game)
        {
            //Handle Graphics...

            if (game == null || game.graphicsX == null)
            {
                return;
            }

            Sprite sprite = spriteStand;
            if (game.velocity < 0)
            {
                sprite = spriteWalkLeft;
            }
            else if (game.velocity > 0)
            {
                sprite = spriteWalkRight;
            }
            game.graphicsX.Draw(sprite, game.x, game.y);


        }

        private Sprite spriteStand;
        private Sprite spriteWalkLeft;
        private Sprite spriteWalkRight;
    }

    #region interfaces

    interface BaseComponent
    {
        void Update(RPGGame game);
    }

    interface GraphicsComponent : BaseComponent
    {
        new void Update(RPGGame game);
    }

    interface PhysicsComponent : BaseComponent
    {
        new void Update(RPGGame game);
    }

    interface InputComponent : BaseComponent
    {
        new void Update(RPGGame game);
    }

    #endregion

    #region Auxiliary class

    class WorldX
    {

    }

    class GraphicsX
    {
        public void Draw(Sprite sprite, float x, float y) { }
    }
}

# endregion

