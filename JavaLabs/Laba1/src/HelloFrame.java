import java.awt.*;
import java.awt.event.*;
import java.util.Random;
/** Simple Frame with Label
 @version 1.0
 @author Mister X
 */
public class HelloFrame extends Frame{
    
    /** Program entry point */
    public static void main(String[] args) throws Exception{
        HelloFrame hello = new HelloFrame();
        hello.setTitle("Приветствие");
        hello.setBackground(Color.GREEN);
        hello.setLayout(new FlowLayout());
        hello.setLabelAttributes(firstLabel);
        hello.setLabelAttributes(secondLabel);
        hello.add(firstLabel);
        hello.add(secondLabel);
        hello.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                Random rnd = new Random();
                int idx = rnd.nextInt(7);
                secondLabel.setText(text[idx]);
                FontMetrics fm = secondLabel.getFontMetrics(secondLabel.getFont());
                secondLabel.setSize(fm.stringWidth(secondLabel.getText())+10,firstLabel.getHeight());
            }
        });
        hello.addWindowListener(new WindowAdapter() {
            @Override
            public void windowClosing(WindowEvent e) {
                System.exit(0);
            }
        });
        hello.setBounds(100,100,960,480);
        hello.setVisible(true);
    }
    /** method that changes Label attributes
     @param label - concrete Label
     */
    private void  setLabelAttributes(Label label){
        Font font = new Font(Font.SANS_SERIF, Font.PLAIN, 22);
        label.setFont(font);
        label.setBackground(Color.BLACK);
        label.setForeground(Color.YELLOW);

    }
    /** field - reference array of strings */

    private static String[] text = {"ВПРщик", "ВИАСовец", "КБшник", "жабист", "плюсист", "питонист","ВИшник"};

    /** field - Label for preamble */
    private static final Label firstLabel = new Label("Добро пожаловать,");
    /** field - Label for variants of answers */
    private static final Label secondLabel = new Label(text[0]);
}
