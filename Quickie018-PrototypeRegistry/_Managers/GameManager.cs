namespace Quickie018;

public class GameManager
{
    private readonly List<Gem> _gems = new();
    private readonly Gem _gem;
    private readonly Gem _coin;
    private readonly Gem _particle;
    private PrototypeRegistry _registry;
    private float _timer = 0;
    private Vector2 startPosition = new Vector2(Globals.Bounds.X, 450);
    public GameManager()
    {
        _registry = new PrototypeRegistry();
        _gem = new(
            Globals.Content.Load<Texture2D>("gem"),
            new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2),
            new(Color.Orange)
        );
        _coin = new(
            Globals.Content.Load<Texture2D>("coin"),
            new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2),
            new(Color.Orange)
        );
        _particle = new(
                       Globals.Content.Load<Texture2D>("particle"),
                        new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2),
                        new(Color.Orange)
        );
        _registry.RegisterPrototype("gem", _gem);
        _registry.RegisterPrototype("coin", _coin);
        _registry.RegisterPrototype("particle", _particle);
        // _gems.Add(_gem);
        // _gems.Add(_coin);



    }

    private void CheckClick()
    {
        
        if (InputManager.Clicked)
        {
            CloneGem(1);
        }

        if (InputManager.RightClicked)
        {
            CloneGem(0);
        }
    }

    private void CloneGem(int type)
    {
        foreach (var gem in _gems)
        {
            if (gem.Rectangle.Intersects(InputManager.MouseRectangle))
            {
                var clone = type > 0 ?
                    (Gem)gem.DeepCloneSkipConstructor() :
                    (Gem)gem.ShallowClone();
                clone.RandomizeDirection();
                //var clone = new Gem(
                //    Globals.Content.Load<Texture2D>("gem"),
                //    gem.Position,
                //    new(gem.GemProperties.Color)
                //);
                if (type > 0) clone.GemProperties.Color = Color.DarkRed;

                _gems.Add(clone);
                return;
            }
        }
    }
    private Gem RandomClone(Vector2 startPosition)
    {

        var clone = (Gem)_registry.GetRandomPrototype();
        clone.Position = startPosition;
        //clone.RandomizeDirection();
        return clone;
    }

    private Gem RandomCloneNormal(Vector2 startPosition)
    {
        int randomNumber = new Random(99999).Next(3);

        // Select a prototype based on the random number
        Gem prototype;
        if (randomNumber == 0)
        {
            prototype = _gem;
        }
        else if (randomNumber == 1)
        {
            prototype = _coin;
        }
        else
        {
            prototype = _particle;
        }

        var clone = (Gem)prototype.DeepCloneSkipConstructor();
        clone.Position = startPosition;
        return clone;
    }
    public void Update()
    {
        InputManager.Update();
        CheckClick();
        _timer += (float)Globals.Time;
        if (_timer >= 2)
        {
            _gems.Add(RandomClone(startPosition));
            _timer = 0;
        }
        if (InputManager.MiddleClicked)
        {
            _gem.GemProperties.Color = Color.LightGreen;
        }
        foreach (var gem in _gems) gem.Update();
    }

    public void Draw()
    {
        Globals.SpriteBatch.Begin();
        foreach (var gem in _gems) gem.Draw();
        Globals.SpriteBatch.End();
    }
}
