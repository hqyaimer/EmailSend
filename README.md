C# EmailSend 邮件发送库
===========================

#### C# EmailSend 邮件发送库，支持QQ邮箱、QQ企业邮箱、163邮箱、新浪邮箱、126邮箱，可自行添加其他邮箱。

开始使用
-------

### 引入EmailSend库

`using Hqyaimer.Email`

Options 选项
----

`ToEmailList` - `List<string>` 类型，收件人列表，默认为空，可自行添加。

`SubjectEncoding` - `Encoding` 类型，邮件主题编码，默认为`UTF-8`。

`BodyEncoding` - `Encoding` 类型，邮件内容编码，默认为`Default`。

`Priority` - `MailPriority` 类型，邮件优先级，默认为`High`。

`IsBodyHtml` - `bool` 类型，是否支持Html格式内容，默认为`true`。

`Attachments` - `List<Attachment>` 类型，附件列表，默认为空，可自行添加。

函数/方法
----

### 发送邮件(收件人列表)

`public bool Send(string title,string content)`

parameters[`title` - 标题 | `content` - 内容]

返回:是否发送成功

### 发送邮件(指定收件人)

`public bool Send(string toemail, string title, string content)`

parameters[`toemail` - 收件邮箱 | `title` - 标题 | `content` - 内容]

返回:是否发送成功
