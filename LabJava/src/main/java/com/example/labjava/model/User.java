package com.example.labjava.model;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class User {
    private int id;
    private String name;
    private String email;
    private String picture;
    private int age;
    private String homeTown;
    private String password;

}