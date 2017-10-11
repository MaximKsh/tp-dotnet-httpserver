tp-dotnet-httpserver:
maxim@maximPC:~$ ab -n 1000 -c 100 http://localhost:5000/httptest/wikipedia_russia.html
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


Server Software:        http-dotnet-server
Server Hostname:        localhost
Server Port:            5000

Document Path:          /httptest/wikipedia_russia.html
Document Length:        954824 bytes

Concurrency Level:      100
Time taken for tests:   0.616 seconds
Complete requests:      1000
Failed requests:        0
Total transferred:      954976000 bytes
HTML transferred:       954824000 bytes
Requests per second:    1624.58 [#/sec] (mean)
Time per request:       61.555 [ms] (mean)
Time per request:       0.616 [ms] (mean, across all concurrent requests)
Transfer rate:          1515069.98 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.7      0       8
Processing:     1   59  21.8     62     132
Waiting:        0   36  19.3     34      97
Total:          4   60  21.7     62     132

Percentage of the requests served within a certain time (ms)
  50%     62
  66%     66
  75%     72
  80%     76
  90%     88
  95%     96
  98%    102
  99%    107
 100%    132 (longest request)



nginx:
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
Time taken for tests:   0.268 seconds
Complete requests:      1000
Failed requests:        0
Total transferred:      955071000 bytes
HTML transferred:       954824000 bytes
Requests per second:    3727.67 [#/sec] (mean)
Time per request:       26.826 [ms] (mean)
Time per request:       0.268 [ms] (mean, across all concurrent requests)
Transfer rate:          3476748.74 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    1   1.3      0       8
Processing:     7   25  13.7     22      76
Waiting:        0    3   7.7      0      38
Total:          8   26  14.5     23      78

Percentage of the requests served within a certain time (ms)
  50%     23
  66%     23
  75%     23
  80%     25
  90%     44
  95%     70
  98%     76
  99%     77
 100%     78 (longest request)

