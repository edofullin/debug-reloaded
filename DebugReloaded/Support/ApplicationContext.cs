﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using DebugReloaded.Commands;
using DebugReloaded.Containers;
using DebugReloaded.Interface;

namespace DebugReloaded.Support {
    public class ApplicationContext {
        //  => Version.Parse(FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion);

        public static readonly bool Verbose = true;

        public static readonly XDocument doc =
            XDocument.Load(
                @"C:\Users\edoardo.fullin\OneDrive\Programmazione\C#\DebugReloaded\DebugReloaded\Commands\AssemblyCommands.xml");

        public static readonly int memSize = 65535;

        public Assembler CommandAssembler;

        public CommandInterpreter Interpreter;

        public Memory MainMemory = new Memory(memSize);

        public List<CommandTemplate> Program = new List<CommandTemplate>();

        public static Version AppVersion
            => AssemblyName.GetAssemblyName(Assembly.GetExecutingAssembly().Location).Version;

        public List<CommandTemplate> CommandTemplList { get; }

        public List<Register> Registers { get; } = new List<Register> {
            new Register("ax"),
            new Register("bx"),
            new Register("cx"),
            new Register("dx"),
            new Register("si"),
            new Register("di"),
            new Register("cs"),
            new Register("ds"),
            new Register("ip")
        };

        public ApplicationContext() {
            Interpreter = new CommandInterpreter(this);
            CommandAssembler = new Assembler(this);
            // TODO REPLACE WITH PARAMS
            CommandTemplate.ctx = this;
            CommandTemplList = CommandTemplate.GetCommandsFromXML(doc);
        }

        public Register GetRegisterByName(string name) {
            return Registers.Find(r => r.Name == name);
        }
    }
}