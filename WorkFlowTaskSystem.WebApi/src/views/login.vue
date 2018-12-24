<style lang="less">
    @import './login.less';
</style>

<template>
    <div class="login" @keydown.enter="handleSubmit">
        <div class="login-con">
            <Card :bordered="false">
                <p slot="title">
                    <Icon type="log-in"></Icon>
                    欢迎登录
                </p>
                <div class="form-con">
                    <Form ref="loginForm" :model="form" :rules="rules">
                        <FormItem prop="userNameOrEmailAddress">
                            <Input v-model="form.userNameOrEmailAddress" placeholder="请输入用户名">
                                <span slot="prepend">
                                    <Icon :size="16" type="person"></Icon>
                                </span>
                            </Input>
                        </FormItem>
                        <FormItem prop="password">
                            <Input type="password" v-model="form.password" placeholder="请输入密码">
                                <span slot="prepend">
                                    <Icon :size="14" type="locked"></Icon>
                                </span>
                            </Input>
                        </FormItem>
                        <FormItem>
                            <Button @click="handleSubmit" type="primary" long>登录</Button>
                        </FormItem>
                    </Form>
                    <p class="login-tip">输入任意用户名和密码即可</p>
                </div>
            </Card>
        </div>
    </div>
</template>

<script>
import Cookies from 'js-cookie';
export default {
    data () {
        return {
            form: {
                userNameOrEmailAddress: '',
                password: '',
                rememberClient:false
            },
            rules: {
                userNameOrEmailAddress: [
                    { required: true, message: '账号不能为空', trigger: 'blur' }
                ],
                password: [
                    { required: true, message: '密码不能为空', trigger: 'blur' }
                ]
            }
        };
    },
    methods: {
        handleSubmit () {
            this.$refs.loginForm.validate((valid) => {
                if (valid) {
                    this.$Message.loading({
                        content: this.L('PleaseWait'),
                        duration:0
                    });
                    let self = this;

                    this.$store.dispatch({
                        type: 'user/login',
                        data: self.form
                     }).then(response => {
                        Cookies.set('user', self.form.userNameOrEmailAddress);
                        location.reload();
                    }, (error) => {
                        
                        this.$Message.destroy();
                    });
                    //Cookies.set('user', this.form.userName);
                    //Cookies.set('password', this.form.password);
                    this.$store.commit('setAvator', 'https://ss1.bdstatic.com/70cFvXSh_Q1YnxGkpoWK1HF6hhy/it/u=3448484253,3685836170&fm=27&gp=0.jpg');
                    //if (this.form.userName === 'iview_admin') {
                    //    Cookies.set('access', 0);
                    //} else {
                     //   Cookies.set('access', 1);
                    //}
                    
                }
            });
        },
        handleSubmitWindow(){
            this.$store.dispatch({
                        type: 'user/loginWindow',
                     }).then(response => {
                        Cookies.set('user', response.data.result.accessToken);
                        if(Cookies.get('user')){
                            location.reload();
                        }
                    }, (error) => {
                        
                        this.$Message.destroy();
            });

        }
    },
    created(){
        this.handleSubmitWindow();
    }
};
</script>

<style>

</style>
