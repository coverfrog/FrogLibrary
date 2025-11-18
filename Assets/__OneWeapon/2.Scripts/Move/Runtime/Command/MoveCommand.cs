using UnityEngine;

public class MoveCommand : ICommand
{
    private MoveHelper _helper;
    private Vector2 _direction;
    private float _speed;
    
    public MoveCommand(MoveHelper helper, Vector2 direction, float speed)
    {
        _speed = speed;
        _direction = direction;
        _helper = helper;
    }
    
    public void Execute()
    {
        // # move
        var rb2d = _helper.component.rb2d;
        
        var originPosition = rb2d.position;
        var addPosition = _direction * (_speed * Time.deltaTime);
        var position = originPosition + addPosition;
        
        rb2d.MovePosition(position);
        
        // # flip
        if (_direction.x == 0) return;
        
        var render = _helper.component.render;

        render.flipX = _direction.x < 0;
    }
}