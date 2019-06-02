import java.awt.Color;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.FontMetrics;
import java.awt.Graphics;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.KeyAdapter;
import java.awt.event.KeyEvent;
import javax.swing.JPanel;
import javax.swing.Timer;
import java.util.ArrayList;

public class Board extends JPanel implements ActionListener {

    private final int B_WIDTH = 300;
    private final int B_HEIGHT = 300;
    private final int DOT_SIZE = 10;
    private final int RAND_POS = 29;
    private final int DELAY = 140;



    private boolean leftDirection = false;
    private boolean rightDirection = true;
    private boolean upDirection = false;
    private boolean downDirection = false;
    private boolean isGameNotOver = true;

    private Timer timer;
    private ArrayList<Item> Snake;
    private Item apple;
    private Item obstacle;
    public Board() {
        
        initBoard();
    }
    
    private void initBoard() {

        addKeyListener(new TAdapter());
        setBackground(Color.black);
        setFocusable(true);

        setPreferredSize(new Dimension(B_WIDTH, B_HEIGHT));
        initGame();
    }

    private void initGame() {

        Snake = new ArrayList<>();
        Snake.add(new Item(50, 50, "resources/head.png"));

        for (int z = 0; z < 3; z++) {
            int x = 50 - z * 10;
            int y = 50;
            Snake.add(new Item(x, y,"resources/dot.png" ));
        }
        
        locateApple();

        timer = new Timer(DELAY, this);
        timer.start();
    }

    @Override
    public void paintComponent(Graphics g) {
        super.paintComponent(g);

        doDrawing(g);
    }
    
    private void doDrawing(Graphics g) {
        
        if (isGameNotOver) {

            apple.draw(g, this);
            for(Item i : Snake)
            {
                i.draw(g, this);
            }
            if(obstacle != null)
                obstacle.draw(g, this);

            Toolkit.getDefaultToolkit().sync();

        } else {

            gameOver(g);
        }        
    }

    private void gameOver(Graphics g) {
        
        String msg = "Game Over";
        Font small = new Font("Helvetica", Font.BOLD, 14);
        FontMetrics metr = getFontMetrics(small);

        g.setColor(Color.white);
        g.setFont(small);
        g.drawString(msg, (B_WIDTH - metr.stringWidth(msg)) / 2, B_HEIGHT / 2);
    }

    private void checkApple() {

        Item head = Snake.get(0);
        Item endOfSnake = Snake.get(Snake.size()-1);
        if(apple.checkCollision(head._x, head._y)) {

            locateApple();
            Snake.add(new Item(endOfSnake._x-10,endOfSnake._y,"resources/dot.png"));
        }
    }

    private void move() {




        for(int i = Snake.size()-1; i > 0; i--)
        {
            Item head = Snake.get(i-1);
            Item last = Snake.get(i);
            last._x = head._x;
            last._y = head._y;
        }



        if (leftDirection) {
            Snake.get(0)._x -= DOT_SIZE;
        }

        if (rightDirection) {
            Snake.get(0)._x += DOT_SIZE;
        }

        if (upDirection) {
            Snake.get(0)._y -= DOT_SIZE;
        }

        if (downDirection) {
            Snake.get(0)._y += DOT_SIZE;
        }
    }

    private void checkCollision() {

        Item head = Snake.get(0);
//        for (int z = Snake.size()-1; z > 0; z--) {
//            Item checking = Snake.get(z);
//
//            if ((head._x == checking._x) && (head._y == checking._y)) {
//                isGameNotOver = false;
//            }
//        }


        if (obstacle != null)
        {
            if(obstacle.checkCollision(head._x,head._y)){
                isGameNotOver = false;
            }
        }

        if (head._y >= B_HEIGHT) {
            isGameNotOver = false;
        }

        if (head._y < 0) {
            isGameNotOver = false;
        }

        if (head._x >= B_WIDTH) {
            isGameNotOver = false;
        }

        if (head._x < 0) {
            isGameNotOver = false;
        }
        
        if (!isGameNotOver) {
            timer.stop();
        }
    }
    public void addObstacle()
    {
        int r_x = (int) (Math.random() * RAND_POS);
        int r_y = (int) (Math.random() * RAND_POS);
        obstacle = new Item((r_x * DOT_SIZE), (r_y * DOT_SIZE), "resources/obstacle.png");
    }

    private void locateApple() {

        int r_x = (int) (Math.random() * RAND_POS);
        int r_y = (int) (Math.random() * RAND_POS);
        apple = new Item((r_x * DOT_SIZE), (r_y * DOT_SIZE), "resources/apple.png");
    }

    @Override
    public void actionPerformed(ActionEvent e) {

        if (isGameNotOver) {

            checkApple();
            checkCollision();
            move();
        }

        repaint();
    }

    private class TAdapter extends KeyAdapter {

        @Override
        public void keyPressed(KeyEvent e) {

            int key = e.getKeyCode();

            if ((key == KeyEvent.VK_LEFT) && (!rightDirection)) {
                leftDirection = true;
                upDirection = false;
                downDirection = false;
            }

            if ((key == KeyEvent.VK_RIGHT) && (!leftDirection)) {
                rightDirection = true;
                upDirection = false;
                downDirection = false;
            }

            if ((key == KeyEvent.VK_UP) && (!downDirection)) {
                upDirection = true;
                rightDirection = false;
                leftDirection = false;
            }

            if ((key == KeyEvent.VK_DOWN) && (!upDirection)) {
                downDirection = true;
                rightDirection = false;
                leftDirection = false;
            }
            if (key == KeyEvent.VK_SPACE ) {
                downDirection = false;
                rightDirection = false;
                leftDirection = false;
            }
        }
    }
}

