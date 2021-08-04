# Client-Server-Networking-Project
  Two C# Windows console applications; one is a client and the other is the server.<br>
  This is a University Assessment Coursework which received a 94% mark.
  
  ---
  ## Requirements
  ### Part 1
  Step 0. The first laboratory task introduced you to the sample client used in the lectures. You
  may need to check about the method `stream.flush` which is not included in the example code.
  
  Step 1. Develop and complete your client to perform a whois request by having it communicate
  with the working server provided by your lecturer ( whois.net.dcs.hull.ac.uk) or an external
  third party internet whois server. This should be based on the code template given in the lecture
  Powerpoint. It is often sensible to be sure of the correctness of your client before proceeding to
  develop your server. It might be a good idea to start using the SVN server to keep track of the
  development of your solution.
  
  Step 2. Develop a minimal server based on the template in the Powerpoint slides that uses the
  whois protocol, using your own client to interface with it. You would need to write code to
  store the person locations in the server memory, and you might find the classes
  for `Hashtable` or `Dictionary` particularly useful for this. We suggest that you initially develop a
  server that does not use threads, to ensure the protocol and communications work properly.
  This can then be augmented to handle multiple clients and multiple protocols later.
  
  Step 3. Extend your basic working client to include the command line arguments of **-h, -p**.
  Add the HTTP protocols and the arguments **-h0, -h1, -h9** and test against a web server.
  
  Step 4. Extend your basic server to recognise HTTP protocol requests and test with your client
  and later with a web browser.
  
  Step 5. Upgrade your server to include handling of multiple threads so concurrent enquiries
  from multiple clients is possible. If you find this difficult to achieve, remember to retain a copy
  of your working non-threaded server to hand-in for assessment should you not have a working
  threaded version. Do not damage your only working server when trying to add more capability,
  or you risk losing all the marks you earned to this point. You might find that using SVN from
  the first week has become valuable is assisting you in recovering earlier versions!
  
  Step 6. Add any remaining optional features to your client and server, such as logging, and
  finish testing. Now prepare to add user interface features to the client and server for part2.
  
  ### Part 2
  
  This requires you to implement a C# Windows Applications for both your client and server.
  Your client and server should be able to operate as specified in part 1 and the additional user
  interface should not change its operation or existing user interface, but should only add more
  functionality.
  
  The client, if launched with no arguments, should now open a Windows interface that permits
  the user to specify the same operations as possible for the command line but with a, perhaps,
  better user interface style and design. You should design that user interface and decide what
  features it should contain based on your learning on the course.
  
  The server, when launched with an argument of –w should open a windowed interface that
  permits an operator to control the functions of the server. If launched with no arguments it
  should operate as previously specified in part 1. If –w is used in conjunction with other
  arguments, these arguments indicate pre-set values that should be shown in the windowed user
  interface, but the operator should be able to change them using the interface you implement.
  
  Note that the client and server each have a different mechanism for launching the windowed
  interface. In the certificate stage programming modules (last year) you were set laboratory task
  in creating windows interfaces using either WPF or Windows Forms. You should refer back to
  the work from last year to help you implement you chosen interface. The lectures on User
  Interface Design will not teach you this programming, but focus on issues of design and
  usability which will occur after this assessment is completed.
  
  This part of the assignment will then permit you to reflect on what makes a good user interface
  and support your learning of user interface design issues.
