package be.pxl.itproject.kbcfoodandgo.servicetests;

import be.pxl.itproject.kbcfoodandgo.models.entities.Log;
import be.pxl.itproject.kbcfoodandgo.models.entities.Menu;
import be.pxl.itproject.kbcfoodandgo.services.PusherServiceImp;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.junit.MockitoJUnitRunner;
import org.springframework.boot.test.context.SpringBootTest;


@RunWith(MockitoJUnitRunner.class)
@SpringBootTest
public class PusherServiceImpTests {

    @Mock
    private PusherServiceImp pusherServiceImp;

    private Log log;
    private Menu menu;

    @Before
    public void setUp(){
        log = new Log();
        menu = new Menu();
    }

    @Test
    public void onTestLogShouldOnlyTriggerOnce(){
        pusherServiceImp.onTestLog(log);
        Mockito.verify(pusherServiceImp, Mockito.times(1)).onTestLog(log);
    }

    @Test
    public void onCreateMenuShouldOnlyTriggerOnce(){
        pusherServiceImp.onCreateMenu();
        Mockito.verify(pusherServiceImp, Mockito.times(1)).onCreateMenu();
    }

}
