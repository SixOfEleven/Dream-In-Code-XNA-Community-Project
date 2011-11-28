using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DreamInCodeLibrary
{
    // enum for the buttons on the mouse. These are not included if the
    // target platform is the Xbox 360
    #region Enumeration Region
#if !XBOX360
    public enum MouseButton { None, Left, Right, Middle, X1, X2 };
#endif
    #endregion

    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public sealed class Xin : Microsoft.Xna.Framework.GameComponent
    {
        /// <summary>
        /// This region holds methods and properties not related to a specific input device
        /// </summary>
        #region General Region

        /// <summary>
        /// This method is used to set the state of all input devices specifc to Windows and
        /// the Xbox 360. If the target platform for the game is the Xbox 360 then support
        /// for the mouse is excluded.
        /// </summary>
        private static void SetStates()
        {
            lastKeyboardState = keyboardState;
#if !XBOX360
            lastMouseState = mouseState;
#endif
            for (int i = 0; i < gamePadState.Length; i++)
                lastGamePadState[i] = gamePadState[i];

            keyboardState = Keyboard.GetState();
#if !XBOX360
            mouseState = Mouse.GetState();
#endif
            gamePadState[0] = GamePad.GetState(PlayerIndex.One);
            gamePadState[1] = GamePad.GetState(PlayerIndex.Two);
            gamePadState[2] = GamePad.GetState(PlayerIndex.Three);
            gamePadState[3] = GamePad.GetState(PlayerIndex.Four);
        }

        #endregion

        public Xin(Game game)
            : base(game)
        {
            keyboardState = Keyboard.GetState();
#if !XBOX360
            mouseState = Mouse.GetState();
#endif
            gamePadState[0] = GamePad.GetState(PlayerIndex.One);
            gamePadState[1] = GamePad.GetState(PlayerIndex.Two);
            gamePadState[2] = GamePad.GetState(PlayerIndex.Three);
            gamePadState[3] = GamePad.GetState(PlayerIndex.Four);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public sealed override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public sealed override void Update(GameTime gameTime)
        {
            SetStates();
            base.Update(gameTime);
        }

        public static bool WasReleased(PlayerIndex? playerInControl, Buttons button, Keys key, out PlayerIndex playerIndex)
        {
            if (playerInControl.HasValue)
            {
                playerIndex = playerInControl.Value;

                if (WasKeyReleased(key) || WasButtonReleased(playerInControl, button, out playerIndex))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return (WasReleased(PlayerIndex.One, button, key, out playerIndex) ||
                    WasReleased(PlayerIndex.Two, button, key, out playerIndex) ||
                    WasReleased(PlayerIndex.Three, button, key, out playerIndex) ||
                    WasReleased(PlayerIndex.Four, button, key, out playerIndex));
            }
        }

        public static bool WasButtonReleased(PlayerIndex? playerInControl, Buttons button, out PlayerIndex playerIndex)
        {
            if (playerInControl.HasValue)
            {
                playerIndex = playerInControl.Value;

                int i = (int)playerIndex;

                return (gamePadState[i].IsButtonUp(button) &&
                    lastGamePadState[i].IsButtonDown(button));
            }
            else
            {
                return (WasButtonReleased(PlayerIndex.One, button, out playerIndex) ||
                    WasButtonReleased(PlayerIndex.Two, button, out playerIndex) ||
                    WasButtonReleased(PlayerIndex.Three, button, out playerIndex) ||
                    WasButtonReleased(PlayerIndex.Four, button, out playerIndex));
            }
        }

        public static bool WasKeyReleased(Keys key)
        {
            if (keyboardState.IsKeyUp(key) &&
                lastKeyboardState.IsKeyDown(key))
                return true;
            return false;
        }

        public static bool WasKeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key) && lastKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// This region holds code specific to the mouse and is only supported in Windows
        /// games. It will be excluded if the target platfrom is the Xbox 360.
        /// </summary>
        #region Mouse Region
#if !XBOX360

        /// <summary>
        /// This region holds code specific to the events for the mouse and is only supported
        /// in Windows game. It will be excluded if the target platform is the Xbox 360.
        /// </summary>
        #region Mouse Event Region

        ///<summary>
        /// Event fired when a mouse button is down
        ///</summary>

        private void CheckMouseDown()
        {
        }

        private void CheckMouseUp()
        {
        }

        private void CheckMouseMove()
        {
        }

        private void CheckMouseClick()
        {
        }

        #endregion

        /// <summary>
        /// These fields hold the current state of the mouse and the state of the mouse in
        /// the last frame of the game. The last frame field is useful in checking for a
        /// single click of a mouse button
        /// </summary>

        static MouseState mouseState;
        static MouseState lastMouseState;

        /// <summary>
        /// Property to return the current state of the mouse
        /// </summary>
        public static MouseState MouseState
        {
            get { return mouseState; }
        }

        /// <summary>
        /// Property to return the state of the mouse in the last frame of the game
        /// </summary>
        public static MouseState LastMouseState
        {
            get { return lastMouseState; }
        }

        /// <summary>
        /// Property to return the location of the mouse pointer as a Point
        /// </summary>
        public static Point MouseAsPoint
        {
            get { return new Point(mouseState.X, mouseState.Y); }
        }

        /// <summary>
        /// Property to return the locatoin of the mouse pointer as a Vector2
        /// </summary>
        public static Vector2 MouseAsVector2
        {
            get { return new Vector2(mouseState.X, mouseState.Y); }
        }

        /// <summary>
        /// Property to return the location of the mouse pointer as a Point in the
        /// last frame of the game
        /// </summary>
        public static Point LastMouseAsPoint
        {
            get { return new Point(lastMouseState.X, lastMouseState.Y); }
        }

        /// <summary>
        /// Property to return the locatoin of the mouse pointer as a Vector2 in the
        /// last frame of the game
        /// </summary>
        public static Vector2 LastMouseAsVector2
        {
            get { return new Vector2(lastMouseState.X, lastMouseState.Y); }
        }

        /// <summary>
        /// Public method that is used to check for a click of a specified button
        /// </summary>
        /// <param name="button">
        /// The mouse button to checked
        /// </param>
        /// <returns>
        /// true if button was down in the last frame and up in this frame, false
        /// otherwise
        /// </returns>
        public static bool CheckMousePress(MouseButton button)
        {
            bool result = false;

            switch (button)
            {
                case MouseButton.Left:
                    result = CheckLeftButtonPress();
                    break;
                case MouseButton.Right:
                    result = CheckRightButtonPress();
                    break;
                case MouseButton.Middle:
                    result = CheckMiddleButtonPress();
                    break;
                case MouseButton.X1:
                    result = CheckXButton1Press();
                    break;
                case MouseButton.X2:
                    result = CheckXButton2Press();
                    break;
            }
            return result;
        }

        /// <summary>
        /// Method to check to see if a mouse button is down
        /// </summary>
        /// <param name="button">
        /// The button to check
        /// </param>
        /// <returns>
        /// true if button is down, false otherwise
        /// </returns>
        public static bool IsMouseDown(MouseButton button)
        {
            bool result = false;

            switch (button)
            {
                case MouseButton.None:
                    break;
                case MouseButton.Left:
                    result = mouseState.LeftButton == ButtonState.Pressed;
                    break;
                case MouseButton.Right:
                    result = mouseState.RightButton == ButtonState.Pressed;
                    break;
                case MouseButton.Middle:
                    result = mouseState.MiddleButton == ButtonState.Pressed;
                    break;
                case MouseButton.X1:
                    result = mouseState.XButton1 == ButtonState.Pressed;
                    break;
                case MouseButton.X2:
                    result = mouseState.XButton2 == ButtonState.Pressed;
                    break;
            }
            return result;
        }

        /// <summary>
        /// Method to return if a mouse button is currently up
        /// </summary>
        /// <param name="button">
        /// The button to be checked
        /// </param>
        /// <returns>
        /// true if the button is up, false otherwise
        /// </returns>
        public static bool IsMouseUp(MouseButton button)
        {
            bool result = false;

            switch (button)
            {
                case MouseButton.Left:
                    result = mouseState.LeftButton == ButtonState.Released;
                    break;
                case MouseButton.Right:
                    result = mouseState.RightButton == ButtonState.Released;
                    break;
                case MouseButton.Middle:
                    result = mouseState.MiddleButton == ButtonState.Released;
                    break;
                case MouseButton.X1:
                    result = mouseState.XButton1 == ButtonState.Released;
                    break;
                case MouseButton.X2:
                    result = mouseState.XButton2 == ButtonState.Released;
                    break;
            }
            return result;
        }

        /// <summary>
        /// Method to check to see if a mouse button was down in the last frame
        /// </summary>
        /// <param name="button">
        /// The button to check
        /// </param>
        /// <returns>
        /// true if button was down, false otherwise
        /// </returns>
        public static bool IsLastMouseDown(MouseButton button)
        {
            bool result = false;

            switch (button)
            {
                case MouseButton.Left:
                    result = lastMouseState.LeftButton == ButtonState.Pressed;
                    break;
                case MouseButton.Right:
                    result = lastMouseState.RightButton == ButtonState.Pressed;
                    break;
                case MouseButton.Middle:
                    result = lastMouseState.MiddleButton == ButtonState.Pressed;
                    break;
                case MouseButton.X1:
                    result = lastMouseState.XButton1 == ButtonState.Pressed;
                    break;
                case MouseButton.X2:
                    result = lastMouseState.XButton2 == ButtonState.Pressed;
                    break;
            }
            return result;
        }

        /// <summary>
        /// Method to return if a mouse button was up in the last frame
        /// </summary>
        /// <param name="button">
        /// The button to be checked
        /// </param>
        /// <returns>
        /// true if the button was up, false otherwise
        /// </returns>
        public static bool LastIsMouseUp(MouseButton button)
        {
            bool result = false;

            switch (button)
            {
                case MouseButton.Left:
                    result = lastMouseState.LeftButton == ButtonState.Released;
                    break;
                case MouseButton.Right:
                    result = lastMouseState.RightButton == ButtonState.Released;
                    break;
                case MouseButton.Middle:
                    result = lastMouseState.MiddleButton == ButtonState.Released;
                    break;
                case MouseButton.X1:
                    result = lastMouseState.XButton1 == ButtonState.Released;
                    break;
                case MouseButton.X2:
                    result = lastMouseState.XButton2 == ButtonState.Released;
                    break;
            }
            return result;
        }

        /// <summary>
        /// Private method to see if the left mouse button has been clicked once
        /// </summary>
        /// <returns>
        /// true if the left mouse button was clicked just once, false otherwise
        /// </returns>
        private static bool CheckLeftButtonPress()
        {
            return (mouseState.LeftButton == ButtonState.Released) &&
                (lastMouseState.LeftButton == ButtonState.Pressed);
        }

        /// <summary>
        /// Private method to see if the right mouse button has been clicked once
        /// </summary>
        /// <returns>
        /// true if the right mouse button was clicked just once, false otherwise
        /// </returns>
        private static bool CheckRightButtonPress()
        {
            return (mouseState.RightButton == ButtonState.Released) &&
                (lastMouseState.RightButton == ButtonState.Pressed);
        }

        /// <summary>
        /// Private method to see if the middle mouse button has been clicked once
        /// </summary>
        /// <returns>
        /// true if the middle mouse button was clicked just once, false otherwise
        /// </returns>
        private static bool CheckMiddleButtonPress()
        {
            return (mouseState.MiddleButton == ButtonState.Released) &&
                (lastMouseState.MiddleButton == ButtonState.Pressed);
        }

        /// <summary>
        /// Private method to see if the extra 1 mouse button has been clicked once
        /// </summary>
        /// <returns>
        /// true if the extra 1 mouse button was clicked just once, false otherwise
        /// </returns>
        private static bool CheckXButton1Press()
        {
            return (mouseState.XButton1 == ButtonState.Released) &&
                (lastMouseState.XButton1 == ButtonState.Pressed);
        }

        /// <summary>
        /// Private method to see if the extra 2 mouse button has been clicked once
        /// </summary>
        /// <returns>
        /// true if the extra 2 mouse button was clicked just once, false otherwise
        /// </returns>
        private static bool CheckXButton2Press()
        {
            return (mouseState.XButton2 == ButtonState.Released) &&
                (lastMouseState.XButton2 == ButtonState.Pressed);
        }
#endif
        #endregion


        /// <summary>
        /// Region of methods that work with the keyboard
        /// </summary>
        #region Keyboard Region

        /// <summary>
        /// Members that are related to keyboard events
        /// </summary>
        #region Keyboard Event Region

        // Code related to the KeyPress event which is fired for every key that is pressed

        private bool CheckPressedKeys()
        {
            bool keyPressed = false;

            foreach (Keys key in keyboardState.GetPressedKeys())
            {
                if (lastKeyboardState.IsKeyUp(key))
                {
                    keyPressed = true;
                }
            }
            return keyPressed;
        }

        private bool CheckReleasedKeys()
        {
            bool keyPressed = false;

            foreach (Keys key in lastKeyboardState.GetPressedKeys())
            {
                if (keyboardState.IsKeyUp(key))
                {
                }
            }
            return keyPressed;
        }

        private void CheckKeysDown()
        {
            bool altState = keyboardState.IsKeyDown(Keys.LeftAlt) ||
                keyboardState.IsKeyDown(Keys.RightAlt);
            bool controlState = keyboardState.IsKeyDown(Keys.LeftControl) ||
                keyboardState.IsKeyDown(Keys.RightControl);
            bool shiftState = keyboardState.IsKeyDown(Keys.LeftShift) ||
                keyboardState.IsKeyDown(Keys.RightShift);
        }

        #endregion

        ///<summary>
        /// Fields to hold the current state of the keyboard and the state of the
        /// keyboard in the last frame of the game
        ///</summary>
        static KeyboardState keyboardState;
        static KeyboardState lastKeyboardState;

        /// <summary>
        /// Field that holds if any key has been pressed once
        /// </summary>

        public static void FlushInput()
        {
            SetStates();
        }

        /// <summary>
        /// Property to return the current state of the keyboard
        /// </summary>
        public static KeyboardState KeyboardState
        {
            get { return keyboardState; }
        }

        /// <summary>
        /// Property to return the state of the keyboard in the last frame of the
        /// game
        /// </summary>
        public static KeyboardState LastKeyboardState
        {
            get { return lastKeyboardState; }
        }

        /// <summary>
        /// Method that checks to see if a key was down in the last frame and up in
        /// the current frame. Used to check to see if a key has been pressed and
        /// released
        /// </summary>
        /// <param name="key">
        /// The key to be checked
        /// </param>
        /// <returns>
        /// true if the key was down in the last frame and up in the current frame
        /// returns false otherwise
        /// </returns>
        public static bool CheckKeyPress(Keys key)
        {
            return keyboardState.IsKeyUp(key) && lastKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Method that checks to see if a key is currently down
        /// </summary>
        /// <param name="key">
        /// The key to be checked
        /// </param>
        /// <returns>
        /// true if the key is down, false otherwise
        /// </returns>
        public static bool IsKeyDown(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Method that checks to see if a key is currently up
        /// </summary>
        /// <param name="key">
        /// The key to be checked
        /// </param>
        /// <returns>
        /// true if the key is up, false otherwise
        /// </returns>
        public static bool IsKeyUp(Keys key)
        {
            return keyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Method that checks to see if a key was down in the last frame
        /// </summary>
        /// <param name="key">
        /// The key to be checked
        /// </param>
        /// <returns>
        /// true if the key was down, false otherwise
        /// </returns>
        public static bool LastIsKeyDown(Keys key)
        {
            return lastKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Method that checks to see if a key was up in the last frame
        /// </summary>
        /// <param name="key">
        /// The key to be checked
        /// </param>
        /// <returns>
        /// true if the key was up, false otherwise
        /// </returns>
        public static bool LastIsKeyUp(Keys key)
        {
            return lastKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Method that returns the keys that are currently down on the keyboard
        /// </summary>
        /// <returns>
        /// array of Keys that are currently down
        /// </returns>
        public static Keys[] GetPressedKeys()
        {
            return keyboardState.GetPressedKeys();
        }

        /// <summary>
        /// Method that returns the keys that were down on the keyboard in the last frame
        /// </summary>
        /// <returns></returns>
        public static Keys[] GetLastPressedKeys()
        {
            return lastKeyboardState.GetPressedKeys();
        }
        #endregion

        /// <summary>
        /// Region of methods and property for the game pads
        /// </summary>
        #region Game Pad Region


        /// <summary>
        /// Fields used to hold the current state of all game pads and the state of the
        /// game pads in the last frame of the game.
        /// </summary>
        static GamePadState[] gamePadState = new GamePadState[4];
        static GamePadState[] lastGamePadState = new GamePadState[4];

        public static GamePadState[] GamePadStates
        {
            get { return gamePadState; }
        }

        /// <summary>
        /// Method to return the state of the game pad being checked
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked.
        /// </param>
        /// <returns>
        /// The state of the game pad being checked
        /// </returns>
        public static GamePadState GamePadState(PlayerIndex index)
        {
            return gamePadState[Index(index)];
        }

        // Overload of the method that returns the state of the game pad
        // for PlayerIndex.One
        public static GamePadState GamePadState()
        {
            return gamePadState[0];
        }

        /// <summary>
        /// Method to return the state of the game pad being checked in the last frame
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked
        /// </param>
        /// <returns>
        /// The state of the game pad in the last frame of the game
        /// </returns>
        public static GamePadState LastGamePadState(PlayerIndex index)
        {
            return lastGamePadState[Index(index)];
        }

        // Overload of the method that returns the state of the game pad in the last
        // frame of the game for PlayerIndex.One
        public static GamePadState LastGamePadState()
        {
            return lastGamePadState[0];
        }

        /// <summary>
        /// Method that returns if the game pad to be checked is connected. This method
        /// needs to be called after the call to the SetStates method.
        /// </summary>
        /// <param name="index">
        /// The index of the game pad in the last frame of the game
        /// </param>
        /// <returns>
        /// true if the game pas is connected, false otherwise.
        /// </returns>
        public static bool IsConnected(PlayerIndex index)
        {
            return gamePadState[Index(index)].IsConnected;
        }

        // Overload of the method that returns if the game pad PlayerIndex.One is
        // connected
        public static bool IsConnected()
        {
            return gamePadState[0].IsConnected;
        }

        /// <summary>
        /// Private method that takes a parameter of type PlayerIndex and returns an in
        /// that corrosponds to position of the game pad in the array of game pads
        /// </summary>
        /// <param name="index">
        /// The index of the game pad of type PlayerIndex
        /// </param>
        /// <returns>
        /// The index of the game pad in the array of game pad states
        /// </returns>
        private static int Index(PlayerIndex index)
        {
            if (index == PlayerIndex.One)
                return 0;
            if (index == PlayerIndex.Two)
                return 1;
            if (index == PlayerIndex.Three)
                return 2;
            return 3;
        }

        /// <summary>
        /// Method that will return the current state of the buttons on the game pad
        /// to be checked.
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked
        /// </param>
        /// <returns>
        /// The current state of the buttons on the game pad to be checked
        /// </returns>
        public static GamePadButtons GamePadButtons(PlayerIndex index)
        {
            return gamePadState[Index(index)].Buttons;
        }

        // Overload that returns the state of the game pad buttons for
        // PlayerIndex.One
        public static GamePadButtons GamePadButtons()
        {
            return gamePadState[0].Buttons;
        }

        /// <summary>
        /// Method that will return the state of the buttons on the game pad
        /// to be checked in the last frame of the game
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked
        /// </param>
        /// <returns>
        /// The state of the buttons on the game pad to be checked in the last
        /// frame of the game
        /// </returns>
        public static GamePadButtons LastGamePadButtons(PlayerIndex index)
        {
            return lastGamePadState[Index(index)].Buttons;
        }

        // Overload that returns the state of the game pad buttons in the last
        // frame of the game for PlayerIndex.One
        public static GamePadButtons LastGamePadButtons()
        {
            return lastGamePadState[0].Buttons;
        }

        /// <summary>
        /// Method that checks to see if a button was pressed and released, a single
        /// button press
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked
        /// </param>
        /// <param name="button">
        /// The button on the game pad to be checked
        /// </param>
        /// <returns>
        /// true if the button was up in the current frame and down in the last frame,
        /// meaning that the button was pressed just once, returns false otherwise
        /// </returns>
        public static bool CheckButtonRelease(PlayerIndex index, Buttons button)
        {
            return gamePadState[Index(index)].IsButtonUp(button) &&
                lastGamePadState[Index(index)].IsButtonDown(button);
        }

        // Overload of the method that checks for a single button press for
        // PlayerIndex.One
        public static bool CheckButtonRelease(Buttons button)
        {
            return gamePadState[0].IsButtonUp(button) &&
                lastGamePadState[0].IsButtonDown(button);
        }

        /// <summary>
        /// Method that checks for a new button press, a button that was up is now down
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked
        /// </param>
        /// <param name="button">
        /// The button to be checked
        /// </param>
        /// <returns>
        /// true if there is a new button press, false otherwise
        /// </returns>
        public static bool CheckButtonPress(PlayerIndex index, Buttons button)
        {
            return gamePadState[Index(index)].IsButtonDown(button) &&
                lastGamePadState[Index(index)].IsButtonUp(button);
        }

        // Overload of the methed that checkes for a new button press for PlayerIndex.One
        public static bool CheckButtonPress(Buttons button)
        {
            return gamePadState[0].IsButtonDown(button) &&
                lastGamePadState[0].IsButtonUp(button);
        }

        /// <summary>
        /// Method that checks to see if a button a game pad is currently down
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked
        /// </param>
        /// <param name="button">
        /// The button on the game pad to be checked
        /// </param>
        /// <returns>
        /// true if the button is currently down, false otherwise
        /// </returns>
        public static bool IsButtonDown(PlayerIndex index, Buttons button)
        {
            return gamePadState[Index(index)].IsButtonDown(button);
        }

        // Overload that returns if the button is currently down
        // for PlayerIndex.One
        public static bool IsButtonDown(Buttons button)
        {
            return gamePadState[0].IsButtonDown(button);
        }

        /// <summary>
        /// Method that checks to see if a button a game pad is currently up
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked
        /// </param>
        /// <param name="button">
        /// The button on the game pad to be checked
        /// </param>
        /// <returns>
        /// true if the button is currently up, false otherwise
        /// </returns>
        public static bool IsButtonUp(PlayerIndex index, Buttons button)
        {
            return gamePadState[Index(index)].IsButtonUp(button);
        }

        // Overload that returns if the button is currently up
        // for PlayerIndex.One
        public static bool IsButtonUp(Buttons button)
        {
            return gamePadState[0].IsButtonUp(button);
        }

        /// <summary>
        /// Method that checks to see if a button a game pad was down in the last
        /// frame of the game
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked
        /// </param>
        /// <param name="button">
        /// The button on the game pad to be checked
        /// </param>
        /// <returns>
        /// true if the button was down in the last frame, false otherwise
        /// </returns>
        public static bool LastIsButtonDown(PlayerIndex index, Buttons button)
        {
            return lastGamePadState[Index(index)].IsButtonDown(button);
        }

        // Overload that returns the if the button was down in the last frame 
        // of the game for PlayerIndex.One
        public static bool LastIsButtonDown(Buttons button)
        {
            return lastGamePadState[0].IsButtonDown(button);
        }

        /// <summary>
        /// Method that checks to see if a button a game pad was up in the last
        /// frame of the game
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked
        /// </param>
        /// <param name="button">
        /// The button on the game pad to be checked
        /// </param>
        /// <returns>
        /// true if the button was up in the last frame, false otherwise
        /// </returns>
        public static bool LastIsButtonUp(PlayerIndex index, Buttons button)
        {
            return lastGamePadState[Index(index)].IsButtonUp(button);
        }

        // Overload that returns if the button was up in the last frame 
        // of the game for PlayerIndex.One
        public static bool LastIsButtonUp(Buttons button)
        {
            return lastGamePadState[0].IsButtonUp(button);
        }

        /// <summary>
        /// Method use to set the vibration speed of the motors for the game pad that
        /// is referenced by index.
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to vibrate
        /// </param>
        /// <returns>
        /// The current state of the buttons on the game pad to be checked
        /// </returns>
        public static void Vibrate(PlayerIndex index, float leftMotor, float rightMotor)
        {
            // Restrict parameters so that the values passed to the method are between
            // 0.0f and 1.0f, the minimum and maximum speeds of the motors in the game
            // pad
            leftMotor = MathHelper.Clamp(leftMotor, 0.0f, 1.0f);
            rightMotor = MathHelper.Clamp(rightMotor, 0.0f, 1.0f);

            // Set the frequency of the vibration of the motors
            GamePad.SetVibration(index, leftMotor, rightMotor);
        }

        // Overload of the method that starts the vibration for PlayerIndex.One
        public static void Vibrate(float leftMotor, float rightMotor)
        {
            // Restrict parameters so that the values passed to the method are between
            // 0.0f and 1.0f, the minimum and maximum speeds of the motors in the game
            // pad
            leftMotor = MathHelper.Clamp(leftMotor, 0.0f, 1.0f);
            rightMotor = MathHelper.Clamp(rightMotor, 0.0f, 1.0f);

            // Set the frequency of the vibration of the motors
            GamePad.SetVibration(PlayerIndex.One, leftMotor, rightMotor);
        }

        /// <summary>
        /// Method to stop the game pad motors from running to selected by the index
        /// </summary>
        /// <param name="index">
        /// The index of the game pad
        /// </param>
        public static void StopVibration(PlayerIndex index)
        {
            GamePad.SetVibration(index, 0f, 0f);
        }

        // Overload that stops the vibration for the game pad PlayerIndex.One
        public static void StopVibration()
        {
            GamePad.SetVibration(0, 0f, 0f);
        }

        /// <summary>
        /// Method that returns the state of the left thumbstick of the selected game
        /// pad
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked
        /// </param>
        /// <returns>
        /// Vector2 representing the left thumbstick
        /// </returns>
        public static Vector2 LeftThumb(PlayerIndex index)
        {
            return gamePadState[Index(index)].ThumbSticks.Left;
        }

        // Overload that returns a Vector2 representing the left thumbstick 
        // in the for PlayerIndex.One
        public static Vector2 LeftThumb()
        {
            return gamePadState[0].ThumbSticks.Left;
        }

        /// <summary>
        /// Method that returns the state of the right thumbstick of the 
        /// selected game pad
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked
        /// </param>
        /// <returns>
        /// Vector2 representing the right thumbstick
        /// </returns>
        public static Vector2 RightThumb(PlayerIndex index)
        {
            return gamePadState[Index(index)].ThumbSticks.Right;
        }

        // Overload that returns a Vector2 representing the right thumbstick in the
        // for PlayerIndex.One
        public static Vector2 RightThumb()
        {
            return gamePadState[0].ThumbSticks.Right;
        }

        /// <summary>
        /// Method that returns the state of the left thumbstick of the selected game
        /// pad in the last frame of the game
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked
        /// </param>
        /// <returns>
        /// Vector2 representing the left thumbstick in the last frame of the game
        /// </returns>
        public static Vector2 LastLeftThumb(PlayerIndex index)
        {
            return lastGamePadState[Index(index)].ThumbSticks.Left;
        }

        // Overload that returns a Vector2 representing the left thumbstick in the
        // last frame of the game for PlayerIndex.One
        public static Vector2 LastLeftThumb()
        {
            return lastGamePadState[0].ThumbSticks.Left;
        }

        /// <summary>
        /// Method that returns the state of the right thumbstick of the selected game
        /// pad in the last frame of the game
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked
        /// </param>
        /// <returns>
        /// Vector2 representing the right thumbstick in the last frame of the game
        /// </returns>
        public static Vector2 LastRightThumb(PlayerIndex index)
        {
            return lastGamePadState[Index(index)].ThumbSticks.Right;
        }

        // Overload that returns a Vector2 representing the right thumbstick in the
        // last frame of the game for PlayerIndex.One
        public static Vector2 LastRightThumb()
        {
            return lastGamePadState[0].ThumbSticks.Right;
        }

        /// <summary>
        /// Method that returns the state of the direction pad on the game pad to be 
        /// checked
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked
        /// </param>
        /// <returns>
        /// the state of the direction pad
        /// </returns>
        public static GamePadDPad DPad(PlayerIndex index)
        {
            return gamePadState[Index(index)].DPad;
        }

        // Overload that returns the state of the direction pad for PlayerIndex.One
        public static GamePadDPad DPad()
        {
            return gamePadState[0].DPad;
        }

        /// <summary>
        /// Method that returns the state of the direction pad on the game pad to be checked
        /// in the last frame of the game
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked
        /// </param>
        /// <returns>
        /// the state of the direction pad in the last frame of the game
        /// </returns>
        public static GamePadDPad LastDPad(PlayerIndex index)
        {
            return lastGamePadState[Index(index)].DPad;
        }

        // Overload that returns the state of the direction pad in the 
        // last frame of the game for PlayerIndex.One
        public static GamePadDPad LastDPad()
        {
            return lastGamePadState[0].DPad;
        }

        /// <summary>
        /// Method that returns the state of the triggers on the game pad to be checked
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked
        /// </param>
        /// <returns>
        /// the state of the triggers
        /// </returns>
        public static GamePadTriggers Triggers(PlayerIndex index)
        {
            return gamePadState[Index(index)].Triggers;
        }

        // Overload that returns the state of the triggers in the game
        // for PlayerIndex.One
        public static GamePadTriggers Triggers()
        {
            return gamePadState[0].Triggers;
        }

        /// <summary>
        /// Method that returns the state of the triggers on the game pad to be checked
        /// in the last frame of the game
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked
        /// </param>
        /// <returns>
        /// the state of the triggers in the last frame of the game
        /// </returns>
        public static GamePadTriggers LastTriggers(PlayerIndex index)
        {
            return lastGamePadState[Index(index)].Triggers;
        }

        // Overload that returns the state of the triggers in the last frame 
        // of the game for PlayerIndex.One
        public static GamePadTriggers LastTriggers()
        {
            return lastGamePadState[0].Triggers;
        }

        /// <summary>
        /// Method that returns the current state of the left trigger of the
        /// game pad that is referenced by index
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked
        /// </param>
        /// <returns>
        /// float between 0.0f and 1.0f for the amount the trigger is pressed
        /// </returns>
        public static float LeftTrigger(PlayerIndex index)
        {
            return gamePadState[Index(index)].Triggers.Left;
        }

        // Overload that returns the current state of the left trigger for
        // PlayerIndex.One
        public static float LeftTrigger()
        {
            return gamePadState[0].Triggers.Left;
        }

        /// <summary>
        /// Method that returns the state of the left trigger of the game pad
        /// that is referenced by index in the last frame of the game
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked
        /// </param>
        /// <returns>
        /// float between 0.0f and 1.0f for the amount the trigger is pressed
        /// </returns>
        public static float LastLeftTrigger(PlayerIndex index)
        {
            return lastGamePadState[Index(index)].Triggers.Left;
        }

        // Overload that returns the state of the left trigger for
        // PlayerIndex.One in the last frame of the game
        public static float LastLeftTrigger()
        {
            return lastGamePadState[0].Triggers.Left;
        }

        /// <summary>
        /// Method that returns the current state of the right trigger of the
        /// game pad that is referenced by index
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked
        /// </param>
        /// <returns>
        /// float between 0.0f and 1.0f for the amount the trigger is pressed
        /// </returns>
        public static float RightTrigger(PlayerIndex index)
        {
            return gamePadState[Index(index)].Triggers.Right;
        }

        // Overload that returns the current state of the right trigger for
        // PlayerIndex.One
        public static float RightTrigger()
        {
            return gamePadState[0].Triggers.Right;
        }

        /// <summary>
        /// Method that returns the state of the right trigger of the game pad
        /// that is referenced by index in the last frame of the game
        /// </summary>
        /// <param name="index">
        /// The index of the game pad to be checked
        /// </param>
        /// <returns>
        /// float between 0.0f and 1.0f for the amount the trigger is pressed
        /// </returns>
        public static float LastRightTrigger(PlayerIndex index)
        {
            return lastGamePadState[Index(index)].Triggers.Right;
        }

        // Overload that returns the state of the right trigger for
        // PlayerIndex.One in the last frame of the game
        public static float LastRightTrigger()
        {
            return lastGamePadState[0].Triggers.Right;
        }
        #endregion
    }
}
