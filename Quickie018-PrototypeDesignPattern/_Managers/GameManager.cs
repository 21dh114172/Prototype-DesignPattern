namespace Quickie018;

public class GameManager
{
    private readonly List<Gem> _gems = new();
    private readonly Gem _gem;
    private Color _baseColor = Color.Aqua;

    public GameManager()
    {
        _gem = new(
            Globals.Content.Load<Texture2D>("gem"),
            new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2),
            new(Color.Orange)
        );

        _gems.Add(_gem);
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
                // var clone = new Gem(
                //     Globals.Content.Load<Texture2D>("gem"),
                //     gem.Position,
                //     new(gem.GemProperties.Color)
                // );
                if (type > 0) clone.GemProperties.Color = Color.DarkRed;

                _gems.Add(clone);
                return;
            }
        }
    }

    public void Update()
    {
        InputManager.Update();
        CheckClick();

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
