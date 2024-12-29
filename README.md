# GVM HTTP Proxy

GVM HTTP Proxy is the .NET 9 Rest API with swagger, which allow to send commends using HTTP and execute them on gvm-cli.

# Prerequisites

Install Greenbone OpenVAS.
I tested it on Kali-linux.

To install Open Vas execute below commands:

    sudo apt-get update
    sudo apt install gvm
    sudo gvm-setup
    sudo gvm-start

Write down your admin password.

To run gvm on external ports edit

    nano /usr/lib/systemd/system/gsad.service

and change line

    ExecStart=/usr/sbin/gsad --foreground --listen 0.0.0.0 --port 9392

For start service automatically edit file

    sudo nano /etc/systemd/system/gvm-start.service

and paste:

    [Unit]
    Description=Start Greenbone Vulnerability Manager
    After=network.target

    [Service]
    ExecStart=/usr/bin/sudo /usr/bin/gvm-start

    [Install]
    WantedBy=multi-user.target

And execute:

    sudo systemctl daemon-reload
    sudo systemctl enable gvm-start.service

GVM is now available on https://your.ip:9392

Add new user:

    sudo useradd -m -s /bin/bash user
    sudo passwd user
    sudo usermod -aG _gvm user
    sudo usermod -aG sudo user

After login to new user you should be able to execute command:

    gvm-cli --gmp-username admin --gmp-password [password-generated-during-setup] socket --xml '<get_version/>'

If everything works you can deploy and use GVM HTTP Proxy

## Configuration

Configuration you can find in appsettings.json

    Authorization:Key - you custom key (must be 256B long)
    Authorization:TokenExpirationInMinutes - token expiration in minutes

## Connection

You can use swagger to test communication
Connect to https://your.ip:5001/swagger

Get token using username and password created during configuration (or any credentials created on web page) (/Auth endpoint)
Place token into authorize on swagger
Use method to /GVM to execute any request

## Examples

    <get_version/>

    <get_targets/>

    <get_port_lists/>

    <get_configs/>

    <create_target>
      <name>test</name>
      <hosts>127.0.0.1</hosts>
      <port_list id='33d0cd82-57c6-11e1-8ed1-406186ea4fc5'/>
    </create_target>

    <create_task>
      <name>Test</name>
      <comment>Comment</comment>
      <config id='daba56c8-73ec-11df-a475-002264764cea'></config>
      <target id='a2d58dee-dc07-4953-b975-db8bb092e214'></target>
    </create_task>

    <start_task task_id='46a3af80-fd50-458c-bd0e-f182ad716a83'/>

    <get_reports report_id='5acf1b67-7eed-480d-aa89-8ca437cd4393'></get_reports>

Other xml request you can find in gvm documentation.

## License

MIT License
