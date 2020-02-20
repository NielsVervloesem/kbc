package be.pxl.itproject.kbcfoodandgo.services.interfaces;

import be.pxl.itproject.kbcfoodandgo.models.entities.Log;

public interface LogManager {
    Iterable<Log> getAllLogs();
    void addLog(Log log);
}
