package be.pxl.itproject.kbcfoodandgo.controllers;


import be.pxl.itproject.kbcfoodandgo.models.entities.Log;
import be.pxl.itproject.kbcfoodandgo.services.interfaces.LogManager;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping("api/log")
public class LogController {

    private final LogManager logManager;

    public LogController(LogManager logManager) {
        this.logManager = logManager;
    }

    @GetMapping(value = "/", produces = "application/json;charset=utf-8")
    public ResponseEntity<Iterable<Log>> getAllLogs() {
        Iterable<Log> allLogs = logManager.getAllLogs();

        if (((List<Log>) allLogs).isEmpty()) {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }

        return new ResponseEntity<>(allLogs, HttpStatus.OK);
    }
}
