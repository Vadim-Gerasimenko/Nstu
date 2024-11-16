#!/bin/bash

replace()
(
   for FILE_NAME in * 
   #Loop through all files in the current directory.
   do  
      NEW_NAME="${FILE_NAME// /_}" 
      #Variable writes the correct filename according to the given pattern.
      if [ "$NEW_NAME" != "$FILE_NAME" ]; then
      #Checking if the original file name matches the template one.
         if [ -e $NEW_NAME ]; then
         #Checking the existence of a file with the same name in a directory.
            echo "$FILE_NAME" was not renamed because "$NEW_NAME" already exists
         else
            echo "$FILE_NAME" renamed to "$NEW_NAME"
         mv "$FILE_NAME" "$NEW_NAME"
         #Renaming a file according to the given pattern.
         fi
      fi
   done
)

menu()
(
   echo -e "\nMENU:\n1. Replacing spaces with underscores\n2. Exit"
   read ENTRY
   case $ENTRY in
      1) replace; menu;;
      #Perform a replacement and return to the menu.
      2) exit;;
      #Completion of the program.
      *) echo "Please enter 1 or 2 !"; menu;; 
      #Error message and return to menu.
   esac   
)

echo -e "Group number: $1\nVariant: $2" 
date; menu #Display date and open menu.