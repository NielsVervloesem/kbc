package be.pxl.itproject.kbcfoodandgo.services;

import be.pxl.itproject.kbcfoodandgo.models.entities.Log;
import be.pxl.itproject.kbcfoodandgo.repositories.LogRepository;
import be.pxl.itproject.kbcfoodandgo.services.interfaces.PusherService;
import be.pxl.itproject.kbcfoodandgo.services.interfaces.LogManager;
import org.springframework.data.domain.Sort;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class LogManagerImp implements LogManager {
    private final LogRepository logRepository;
    private final PusherService pusherService;

    public LogManagerImp(LogRepository logRepository, PusherService pusherService) {
        this.logRepository = logRepository;
        this.pusherService = pusherService;
    }

    @Override
    public List<Log> getAllLogs() {
        return logRepository.findAll(Sort.by(Sort.Direction.DESC, "id"));
    }

    @Override
    public void addLog(Log log) {
        pusherService.onTestLog(log);
        logRepository.save(log);
    }
}
