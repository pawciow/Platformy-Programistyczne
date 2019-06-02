import javax.swing.*;
import java.awt.*;
import java.awt.image.ImageObserver;

public class Item {

    private int _x;
    private int _y;
    private Image apple;

    public Item(int x, int y, String imageFileName)
    {
        _x = x;
        _y = y;
        ImageIcon iia = new ImageIcon(imageFileName);
        apple = iia.getImage();
    }
    public void draw(Graphics g, ImageObserver observers)
    {
        g.drawImage(apple, _x, _y, observers);

    }
    public boolean checkCollision(int x, int y)
    {
        return (x == this._x) && (y == this._y);
    }


}