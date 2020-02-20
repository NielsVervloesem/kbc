package be.pxl.itproject.kbcfoodandgo.controllertest;

import be.pxl.itproject.kbcfoodandgo.controllers.LogController;
import be.pxl.itproject.kbcfoodandgo.models.entities.Log;
import be.pxl.itproject.kbcfoodandgo.services.LogManagerImp;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import static org.mockito.Mockito.*;
import static org.springframework.http.MediaType.APPLICATION_JSON_UTF8;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;

@RunWith(SpringRunner.class)
@WebMvcTest(LogController.class)
public class LogControllerTest {

    @Autowired
    private MockMvc mockMvc;

    @MockBean
    private LogManagerImp logManagerImp;

    private Log log1;
    private Log log2;
    private List<Log> returnLogList;

    @Before
    public void setUp() {
        log1 = new Log() {{
            setId(1);
            setMessage("Test message log 1");
            setDate(new Date());
        }};

        log2 = new Log() {{
            setId(2);
            setMessage("Test message log 2");
            setDate(new Date());
        }};

        returnLogList = new ArrayList<>();
        returnLogList.add(log1);
        returnLogList.add(log2);
    }

    @Test
    public void getAlllogsShouldReturnAllLogs() throws Exception {
        when(logManagerImp.getAllLogs()).thenReturn(returnLogList);

        mockMvc
                .perform(get("/api/log/"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(APPLICATION_JSON_UTF8));

        verify(logManagerImp, times(1)).getAllLogs();
        verifyNoMoreInteractions(logManagerImp);
    }


    @Test
    public void getAllLogsWhenNoLogsArePresentReturnsNotFound() throws Exception {
        mockMvc
                .perform(get("/api/log/"))
                .andExpect(status().isNotFound());

        verify(logManagerImp, times(1)).getAllLogs();
        verifyNoMoreInteractions(logManagerImp);
    }
}
