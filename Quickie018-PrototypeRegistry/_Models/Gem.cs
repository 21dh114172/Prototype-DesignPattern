namespace Quickie018;

public class Gem : IPrototype
{
    private static readonly Random r = new();
    private readonly Texture2D _texture;
    private Vector2 _position;
    private Vector2 _direction;
    public Vector2 Position 
    { 
        get => _position; 
        set => _position = value; 
    }
    public Rectangle Rectangle => new((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
    public GemProperties GemProperties { get; }

    public Gem(Texture2D texture, Vector2 position, GemProperties properties)
    {
        //Thread.Sleep(5000);
        _texture = texture;
        GemProperties = properties;
        _position = position;
        RandomizeDirection();
    }

    private Gem(Gem gem)
    {
        _texture = gem._texture;
        GemProperties = new(gem.GemProperties.Color);
        _position = gem._position;
        RandomizeDirection();
    }

    public void RandomizeDirection()
    {
        var angle = r.Next(360);
        _direction = new((float)Math.Sin(angle), -(float)Math.Cos(angle));
    }

    public IPrototype ShallowClone()
    {
        return (Gem)MemberwiseClone();
    }

    public IPrototype DeepClone()
    {
        return new Gem(_texture, _position, new(GemProperties.Color));
    }

    public IPrototype DeepCloneSkipConstructor()
    {
        return new Gem(this);
    }


    public void Update()
    {
        _position += _direction * Globals.Time * 200;
        if (_position.X < 0 || _position.X > Globals.Bounds.X - _texture.Width) _direction = new(-_direction.X, _direction.Y);
        if (_position.Y < 0 || _position.Y > Globals.Bounds.Y - _texture.Height) _direction = new(_direction.X, -_direction.Y);
        _position = new(MathHelper.Clamp(_position.X, 0, Globals.Bounds.X - _texture.Width),
                       MathHelper.Clamp(_position.Y, 0, Globals.Bounds.Y - _texture.Height));
    }

    public void Draw()
    {
        Globals.SpriteBatch.Draw(_texture, _position, GemProperties.Color);
    }
}
