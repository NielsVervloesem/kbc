package be.pxl.itproject.kbcfoodandgo.managertest;

import be.pxl.itproject.kbcfoodandgo.models.entities.Log;
import be.pxl.itproject.kbcfoodandgo.repositories.LogRepository;
import be.pxl.itproject.kbcfoodandgo.services.LogManagerImp;
import be.pxl.itproject.kbcfoodandgo.services.PusherServiceImp;
import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.junit.MockitoJUnitRunner;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.data.domain.Sort;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.when;


@RunWith(MockitoJUnitRunner.class)
@SpringBootTest
public class LogManagerTests {
    @InjectMocks
    private LogManagerImp logManagerImp;

    @Mock
    private LogRepository logRepository;
    @Mock
    private PusherServiceImp pusherServiceImp;


    private List<Log> expectedReturnLog;
    private Log testLog1;
    private Log testLog2;

    @Before
    public void setUp() {
        expectedReturnLog = new ArrayList<>();

        testLog1 = new Log.Builder(1)
                .withMessage("een test log bericht 1")
                .atDate(new Date())
                .build();

        testLog2 = new Log.Builder(2)
                .withMessage("een test log bericht 2")
                .atDate(new Date())
                .build();

        expectedReturnLog.add(testLog1);
        expectedReturnLog.add(testLog2);
        when(logRepository.findAll(Sort.by(Sort.Direction.DESC, "id"))).thenReturn(expectedReturnLog);

    }


    @Test
    public void getAllLogsShouldReturnAllLogs() {
        when(logRepository.findAll(Sort.by(Sort.Direction.DESC, "id"))).thenReturn(expectedReturnLog);

        List<Log> realReturnLogs = logManagerImp.getAllLogs();

        Assert.assertNotNull(realReturnLogs);
        Assert.assertEquals(realReturnLogs.size(), expectedReturnLog.size());
        Mockito.verify(logRepository, Mockito.times(1)).findAll((Sort.by(Sort.Direction.DESC, "id")));
        Mockito.verifyNoMoreInteractions(logRepository);
    }


    @Test
    public void addLogShouldAddAnLogInThedatabase() {
        logManagerImp.addLog(testLog1);
        Assert.assertTrue(expectedReturnLog.contains(testLog1));
        verify(logRepository, Mockito.times(1)).save(testLog1);
        verify(pusherServiceImp, Mockito.times(1)).onTestLog(testLog1);
    }

}
