# i5-6200U, DDR4-2133, SSD

maxim@maximPC:~$ ab -n 1000 -c 100 http://localhost:80/httptest/wikipedia_russia.html
This is ApacheBench, Version 2.3 <$Revision: 1757674 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 100 requests
Completed 200 requests
Completed 300 requests
Completed 400 requests
Completed 500 requests
Completed 600 requests
Completed 700 requests
Completed 800 requests
Completed 900 requests
Completed 1000 requests
Finished 1000 requests


Server Software:        nginx/1.10.3
Server Hostname:        localhost
Server Port:            80

Document Path:          /httptest/wikipedia_russia.html
Document Length:        954824 bytes

Concurrency Level:      100
Time taken for tests:   0.233 seconds
Complete requests:      1000
Failed requests:        0
Total transferred:      955071000 bytes
HTML transferred:       954824000 bytes
Requests per second:    4296.75 [#/sec] (mean)
Time per request:       23.273 [ms] (mean)
Time per request:       0.233 [ms] (mean, across all concurrent requests)
Transfer rate:          4007521.56 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    1   1.7      0       8
Processing:     8   22   6.0     21      45
Waiting:        0    2   6.2      0      27
Total:          8   23   6.9     21      47

Percentage of the requests served within a certain time (ms)
  50%     21
  66%     21
  75%     22
  80%     22
  90%     29
  95%     44
  98%     46
  99%     47
 100%     47 (longest request)
maxim@maximPC:~$ ab -n 1000 -c 100 http://localhost:80/httptest/wikipedia_russia.html
This is ApacheBench, Version 2.3 <$Revision: 1757674 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 100 requests
Completed 200 requests
Completed 300 requests
Completed 400 requests
Completed 500 requests
Completed 600 requests
Completed 700 requests
Completed 800 requests
Completed 900 requests
Completed 1000 requests
Finished 1000 requests


Server Software:        nginx/1.10.3
Server Hostname:        localhost
Server Port:            80

Document Path:          /httptest/wikipedia_russia.html
Document Length:        954824 bytes

Concurrency Level:      100
Time taken for tests:   0.231 seconds
Complete requests:      1000
Failed requests:        0
Total transferred:      955071000 bytes
HTML transferred:       954824000 bytes
Requests per second:    4328.69 [#/sec] (mean)
Time per request:       23.102 [ms] (mean)
Time per request:       0.231 [ms] (mean, across all concurrent requests)
Transfer rate:          4037306.88 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    1   1.2      0       6
Processing:     7   22   6.4     21      46
Waiting:        0    2   5.6      0      27
Total:          8   23   7.2     21      48

Percentage of the requests served within a certain time (ms)
  50%     21
  66%     22
  75%     22
  80%     22
  90%     28
  95%     43
  98%     47
  99%     47
 100%     48 (longest request)
maxim@maximPC:~$ ab -n 1000 -c 100 http://localhost:80/httptest/wikipedia_russia.html
This is ApacheBench, Version 2.3 <$Revision: 1757674 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 100 requests
Completed 200 requests
Completed 300 requests
Completed 400 requests
Completed 500 requests
Completed 600 requests
Completed 700 requests
Completed 800 requests
Completed 900 requests
Completed 1000 requests
Finished 1000 requests


Server Software:        nginx/1.10.3
Server Hostname:        localhost
Server Port:            80

Document Path:          /httptest/wikipedia_russia.html
Document Length:        954824 bytes

Concurrency Level:      100
Time taken for tests:   0.207 seconds
Complete requests:      1000
Failed requests:        0
Total transferred:      955071000 bytes
HTML transferred:       954824000 bytes
Requests per second:    4840.79 [#/sec] (mean)
Time per request:       20.658 [ms] (mean)
Time per request:       0.207 [ms] (mean, across all concurrent requests)
Transfer rate:          4514936.36 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.4      0       2
Processing:     3   20   3.8     20      31
Waiting:        0    1   2.3      0      12
Total:          6   20   3.7     21      32

Percentage of the requests served within a certain time (ms)
  50%     21
  66%     21
  75%     21
  80%     22
  90%     22
  95%     27
  98%     30
  99%     31
 100%     32 (longest request)
maxim@maximPC:~$ ab -n 1000 -c 100 http://localhost:80/httptest/wikipedia_russia.html
This is ApacheBench, Version 2.3 <$Revision: 1757674 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 100 requests
Completed 200 requests
Completed 300 requests
Completed 400 requests
Completed 500 requests
Completed 600 requests
Completed 700 requests
Completed 800 requests
Completed 900 requests
Completed 1000 requests
Finished 1000 requests


Server Software:        nginx/1.10.3
Server Hostname:        localhost
Server Port:            80

Document Path:          /httptest/wikipedia_russia.html
Document Length:        954824 bytes

Concurrency Level:      100
Time taken for tests:   0.215 seconds
Complete requests:      1000
Failed requests:        0
Total transferred:      955071000 bytes
HTML transferred:       954824000 bytes
Requests per second:    4652.96 [#/sec] (mean)
Time per request:       21.492 [ms] (mean)
Time per request:       0.215 [ms] (mean, across all concurrent requests)
Transfer rate:          4339752.20 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.3      0       2
Processing:     4   21   3.6     22      31
Waiting:        0    1   2.0      0      11
Total:          6   21   3.6     22      32

Percentage of the requests served within a certain time (ms)
  50%     22
  66%     22
  75%     23
  80%     23
  90%     23
  95%     26
  98%     29
  99%     31
 100%     32 (longest request)
maxim@maximPC:~$ 

