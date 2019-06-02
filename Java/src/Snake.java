import java.awt.EventQueue;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import javax.swing.JFrame;
import javax.swing.*;

public class Snake extends JFrame implements ActionListener
{
    JMenu menu, submenu;
    JMenuItem i1, i2, i3, i4, i5;
    JMenuBar mb=new JMenuBar();

    public Snake() {
        
        initUI();
        initMenu();
    }
    
    private void initUI() {
        
        add(new Board());

        setResizable(false);
        pack();
        
        setTitle("Snake");
        setLocationRelativeTo(null);
    }

    private void initMenu()
    {
        menu=new JMenu("Menu");
        submenu=new JMenu("Sub Menu");
        i1=new JMenuItem("New game");
        i1.addActionListener(this);
        i2=new JMenuItem("Save");
        i2.addActionListener(this);
        i3=new JMenuItem("Load");
//        i4=new JMenuItem("Item 4");
//        i5=new JMenuItem("Item 5");
        menu.add(i1); menu.add(i2); menu.add(i3);
//        submenu.add(i4); submenu.add(i5);
        menu.add(submenu);
        mb.add(menu);
        setJMenuBar(mb);
    }


    @Override
    public void actionPerformed(ActionEvent e)
    {

        if(e.getSource() == i1)
        {
            System.out.println("Numer 1");
            Snake snake = new Snake();
            snake.setVisible(true);
        }

        if(e.getSource() == i2)
            System.out.println("Numer 2");
    }

    public static void main(String[] args) {
        
        EventQueue.invokeLater(() -> {
            JFrame ex = new Snake();

            ex.setVisible(true);
        });
    }
}
